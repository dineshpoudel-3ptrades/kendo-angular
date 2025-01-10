using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using abp.TestTemplates;
using Volo.Abp.Content;
using abp.Shared;

namespace abp.Controllers.TestTemplates
{
    [RemoteService]
    [Area("app")]
    [ControllerName("TestTemplate")]
    [Route("api/app/test-templates")]

    public abstract class TestTemplateControllerBase : AbpController
    {
        protected ITestTemplatesAppService _testTemplatesAppService;

        public TestTemplateControllerBase(ITestTemplatesAppService testTemplatesAppService)
        {
            _testTemplatesAppService = testTemplatesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<TestTemplateDto>> GetListAsync(GetTestTemplatesInput input)
        {
            return _testTemplatesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TestTemplateDto> GetAsync(Guid id)
        {
            return _testTemplatesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<TestTemplateDto> CreateAsync(TestTemplateCreateDto input)
        {
            return _testTemplatesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TestTemplateDto> UpdateAsync(Guid id, TestTemplateUpdateDto input)
        {
            return _testTemplatesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _testTemplatesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestTemplateExcelDownloadDto input)
        {
            return _testTemplatesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _testTemplatesAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> testtemplateIds)
        {
            return _testTemplatesAppService.DeleteByIdsAsync(testtemplateIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetTestTemplatesInput input)
        {
            return _testTemplatesAppService.DeleteAllAsync(input);
        }
    }
}