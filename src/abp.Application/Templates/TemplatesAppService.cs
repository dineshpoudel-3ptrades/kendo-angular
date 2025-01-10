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
using abp.Templates;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using abp.Shared;

namespace abp.Templates
{
    [RemoteService(IsEnabled = false)]
    [Authorize(abpPermissions.Templates.Default)]
    public abstract class TemplatesAppServiceBase : abpAppService
    {
        protected IDistributedCache<TemplateDownloadTokenCacheItem, string> _downloadTokenCache;
        protected ITemplateRepository _templateRepository;
        protected TemplateManager _templateManager;

        public TemplatesAppServiceBase(ITemplateRepository templateRepository, TemplateManager templateManager, IDistributedCache<TemplateDownloadTokenCacheItem, string> downloadTokenCache)
        {
            _downloadTokenCache = downloadTokenCache;
            _templateRepository = templateRepository;
            _templateManager = templateManager;

        }

        public virtual async Task<PagedResultDto<TemplateDto>> GetListAsync(GetTemplatesInput input)
        {
            var totalCount = await _templateRepository.GetCountAsync(input.FilterText, input.name, input.numberMin, input.numberMax, input.description);
            var items = await _templateRepository.GetListAsync(input.FilterText, input.name, input.numberMin, input.numberMax, input.description, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TemplateDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Template>, List<TemplateDto>>(items)
            };
        }

        public virtual async Task<TemplateDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Template, TemplateDto>(await _templateRepository.GetAsync(id));
        }

        [Authorize(abpPermissions.Templates.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _templateRepository.DeleteAsync(id);
        }

        [Authorize(abpPermissions.Templates.Create)]
        public virtual async Task<TemplateDto> CreateAsync(TemplateCreateDto input)
        {

            var template = await _templateManager.CreateAsync(
            input.number, input.name, input.description
            );

            return ObjectMapper.Map<Template, TemplateDto>(template);
        }

        [Authorize(abpPermissions.Templates.Edit)]
        public virtual async Task<TemplateDto> UpdateAsync(Guid id, TemplateUpdateDto input)
        {

            var template = await _templateManager.UpdateAsync(
            id,
            input.number, input.name, input.description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Template, TemplateDto>(template);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TemplateExcelDownloadDto input)
        {
            var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _templateRepository.GetListAsync(input.FilterText, input.name, input.numberMin, input.numberMax, input.description);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Template>, List<TemplateExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Templates.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(abpPermissions.Templates.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> templateIds)
        {
            await _templateRepository.DeleteManyAsync(templateIds);
        }

        [Authorize(abpPermissions.Templates.Delete)]
        public virtual async Task DeleteAllAsync(GetTemplatesInput input)
        {
            await _templateRepository.DeleteAllAsync(input.FilterText, input.name, input.numberMin, input.numberMax, input.description);
        }
        public virtual async Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new TemplateDownloadTokenCacheItem { Token = token },
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