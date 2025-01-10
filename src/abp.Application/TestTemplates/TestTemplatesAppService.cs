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
using abp.TestTemplates;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using abp.Shared;

namespace abp.TestTemplates
{
    [RemoteService(IsEnabled = false)]
    [Authorize(abpPermissions.TestTemplates.Default)]
    public abstract class TestTemplatesAppServiceBase : abpAppService
    {
        protected IDistributedCache<TestTemplateDownloadTokenCacheItem, string> _downloadTokenCache;
        protected ITestTemplateRepository _testTemplateRepository;
        protected TestTemplateManager _testTemplateManager;

        public TestTemplatesAppServiceBase(ITestTemplateRepository testTemplateRepository, TestTemplateManager testTemplateManager, IDistributedCache<TestTemplateDownloadTokenCacheItem, string> downloadTokenCache)
        {
            _downloadTokenCache = downloadTokenCache;
            _testTemplateRepository = testTemplateRepository;
            _testTemplateManager = testTemplateManager;

        }

        public virtual async Task<PagedResultDto<TestTemplateDto>> GetListAsync(GetTestTemplatesInput input)
        {
            var totalCount = await _testTemplateRepository.GetCountAsync(input.FilterText, input.name, input.numberMin, input.numberMax, input.description);
            var items = await _testTemplateRepository.GetListAsync(input.FilterText, input.name, input.numberMin, input.numberMax, input.description, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TestTemplateDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TestTemplate>, List<TestTemplateDto>>(items)
            };
        }

        public virtual async Task<TestTemplateDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<TestTemplate, TestTemplateDto>(await _testTemplateRepository.GetAsync(id));
        }

        [Authorize(abpPermissions.TestTemplates.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _testTemplateRepository.DeleteAsync(id);
        }

        [Authorize(abpPermissions.TestTemplates.Create)]
        public virtual async Task<TestTemplateDto> CreateAsync(TestTemplateCreateDto input)
        {

            var testTemplate = await _testTemplateManager.CreateAsync(
            input.number, input.name, input.description
            );

            return ObjectMapper.Map<TestTemplate, TestTemplateDto>(testTemplate);
        }

        [Authorize(abpPermissions.TestTemplates.Edit)]
        public virtual async Task<TestTemplateDto> UpdateAsync(Guid id, TestTemplateUpdateDto input)
        {

            var testTemplate = await _testTemplateManager.UpdateAsync(
            id,
            input.number, input.name, input.description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<TestTemplate, TestTemplateDto>(testTemplate);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestTemplateExcelDownloadDto input)
        {
            var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _testTemplateRepository.GetListAsync(input.FilterText, input.name, input.numberMin, input.numberMax, input.description);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<TestTemplate>, List<TestTemplateExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "TestTemplates.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(abpPermissions.TestTemplates.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> testtemplateIds)
        {
            await _testTemplateRepository.DeleteManyAsync(testtemplateIds);
        }

        [Authorize(abpPermissions.TestTemplates.Delete)]
        public virtual async Task DeleteAllAsync(GetTestTemplatesInput input)
        {
            await _testTemplateRepository.DeleteAllAsync(input.FilterText, input.name, input.numberMin, input.numberMax, input.description);
        }
        public virtual async Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new TestTemplateDownloadTokenCacheItem { Token = token },
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