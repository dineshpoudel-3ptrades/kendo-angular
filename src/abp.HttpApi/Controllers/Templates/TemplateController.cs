using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using abp.Templates;
using Volo.Abp.Content;
using abp.Shared;

namespace abp.Controllers.Templates
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Template")]
    [Route("api/app/templates")]

    public abstract class TemplateControllerBase : AbpController
    {
        protected ITemplatesAppService _templatesAppService;

        public TemplateControllerBase(ITemplatesAppService templatesAppService)
        {
            _templatesAppService = templatesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<TemplateDto>> GetListAsync(GetTemplatesInput input)
        {
            return _templatesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TemplateDto> GetAsync(Guid id)
        {
            return _templatesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<TemplateDto> CreateAsync(TemplateCreateDto input)
        {
            return _templatesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TemplateDto> UpdateAsync(Guid id, TemplateUpdateDto input)
        {
            return _templatesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _templatesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(TemplateExcelDownloadDto input)
        {
            return _templatesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _templatesAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> templateIds)
        {
            return _templatesAppService.DeleteByIdsAsync(templateIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetTemplatesInput input)
        {
            return _templatesAppService.DeleteAllAsync(input);
        }
    }
}