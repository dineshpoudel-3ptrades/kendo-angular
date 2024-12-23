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
using abp.Parts;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using abp.Shared;

namespace abp.Parts
{
    [RemoteService(IsEnabled = false)]
    [Authorize(abpPermissions.Parts.Default)]
    public abstract class PartsAppServiceBase : abpAppService
    {
        protected IDistributedCache<PartDownloadTokenCacheItem, string> _downloadTokenCache;
        protected IPartRepository _partRepository;
        protected PartManager _partManager;

        public PartsAppServiceBase(IPartRepository partRepository, PartManager partManager, IDistributedCache<PartDownloadTokenCacheItem, string> downloadTokenCache)
        {
            _downloadTokenCache = downloadTokenCache;
            _partRepository = partRepository;
            _partManager = partManager;

        }

        public virtual async Task<PagedResultDto<PartDto>> GetListAsync(GetPartsInput input)
        {
            var totalCount = await _partRepository.GetCountAsync(input.FilterText, input.name, input.numberMin, input.numberMax);
            var items = await _partRepository.GetListAsync(input.FilterText, input.name, input.numberMin, input.numberMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PartDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Part>, List<PartDto>>(items)
            };
        }

        public virtual async Task<PartDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Part, PartDto>(await _partRepository.GetAsync(id));
        }

        [Authorize(abpPermissions.Parts.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _partRepository.DeleteAsync(id);
        }

        [Authorize(abpPermissions.Parts.Create)]
        public virtual async Task<PartDto> CreateAsync(PartCreateDto input)
        {

            var part = await _partManager.CreateAsync(
            input.number, input.name
            );

            return ObjectMapper.Map<Part, PartDto>(part);
        }

        [Authorize(abpPermissions.Parts.Edit)]
        public virtual async Task<PartDto> UpdateAsync(Guid id, PartUpdateDto input)
        {

            var part = await _partManager.UpdateAsync(
            id,
            input.number, input.name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Part, PartDto>(part);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PartExcelDownloadDto input)
        {
            var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _partRepository.GetListAsync(input.FilterText, input.name, input.numberMin, input.numberMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Part>, List<PartExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Parts.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(abpPermissions.Parts.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> partIds)
        {
            await _partRepository.DeleteManyAsync(partIds);
        }

        [Authorize(abpPermissions.Parts.Delete)]
        public virtual async Task DeleteAllAsync(GetPartsInput input)
        {
            await _partRepository.DeleteAllAsync(input.FilterText, input.name, input.numberMin, input.numberMax);
        }
        public virtual async Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new PartDownloadTokenCacheItem { Token = token },
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