using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using abp.Tests;
using Volo.Abp.Content;
using abp.Shared;

namespace abp.Controllers.Tests
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Test")]
    [Route("api/app/tests")]

    public abstract class TestControllerBase : AbpController
    {
        protected ITestsAppService _testsAppService;

        public TestControllerBase(ITestsAppService testsAppService)
        {
            _testsAppService = testsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<TestDto>> GetListAsync(GetTestsInput input)
        {
            return _testsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TestDto> GetAsync(Guid id)
        {
            return _testsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<TestDto> CreateAsync(TestCreateDto input)
        {
            return _testsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TestDto> UpdateAsync(Guid id, TestUpdateDto input)
        {
            return _testsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _testsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestExcelDownloadDto input)
        {
            return _testsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _testsAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> testIds)
        {
            return _testsAppService.DeleteByIdsAsync(testIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetTestsInput input)
        {
            return _testsAppService.DeleteAllAsync(input);
        }
    }
}