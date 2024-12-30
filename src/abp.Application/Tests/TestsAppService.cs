using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using abp.Permissions;
using abp.Tests;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using abp.Shared;

namespace abp.Tests
{
    [RemoteService(IsEnabled = false)]
    [Authorize(abpPermissions.Tests.Default)]
    public abstract class TestsAppServiceBase : abpAppService
    {
        protected IDistributedCache<TestDownloadTokenCacheItem, string> _downloadTokenCache;
        protected ITestRepository _testRepository;
        protected TestManager _testManager;

        public TestsAppServiceBase(ITestRepository testRepository, TestManager testManager, IDistributedCache<TestDownloadTokenCacheItem, string> downloadTokenCache)
        {
            _downloadTokenCache = downloadTokenCache;
            _testRepository = testRepository;
            _testManager = testManager;

        }

        public virtual async Task<PagedResultDto<TestDto>> GetListAsync(GetTestsInput input)
        {
            var totalCount = await _testRepository.GetCountAsync(input.FilterText, input.name, input.AgeMin, input.AgeMax, input.DOBMin, input.DOBMax);
            var items = await _testRepository.GetListAsync(input.FilterText, input.name, input.AgeMin, input.AgeMax, input.DOBMin, input.DOBMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TestDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Test>, List<TestDto>>(items)
            };
        }

        public virtual async Task<TestDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Test, TestDto>(await _testRepository.GetAsync(id));
        }

        [Authorize(abpPermissions.Tests.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _testRepository.DeleteAsync(id);
        }

        [Authorize(abpPermissions.Tests.Create)]
        public virtual async Task<TestDto> CreateAsync(TestCreateDto input)
        {

            var test = await _testManager.CreateAsync(
            input.Age, input.DOB, input.name
            );

            return ObjectMapper.Map<Test, TestDto>(test);
        }

        [Authorize(abpPermissions.Tests.Edit)]
        public virtual async Task<TestDto> UpdateAsync(Guid id, TestUpdateDto input)
        {

            var test = await _testManager.UpdateAsync(
            id,
            input.Age, input.DOB, input.name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Test, TestDto>(test);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestExcelDownloadDto input)
        {
            var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _testRepository.GetListAsync(input.FilterText, input.name, input.AgeMin, input.AgeMax, input.DOBMin, input.DOBMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Test>, List<TestExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Tests.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(abpPermissions.Tests.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> testIds)
        {
            await _testRepository.DeleteManyAsync(testIds);
        }

        [Authorize(abpPermissions.Tests.Delete)]
        public virtual async Task DeleteAllAsync(GetTestsInput input)
        {
            await _testRepository.DeleteAllAsync(input.FilterText, input.name, input.AgeMin, input.AgeMax, input.DOBMin, input.DOBMax);
        }
        public virtual async Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new TestDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new abp.Shared.DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}