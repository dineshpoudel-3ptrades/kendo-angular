using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using abp.TestTemplates;

namespace abp.Controllers.TestTemplates
{
    [RemoteService]
    [Area("app")]
    [ControllerName("TestTemplate")]
    [Route("api/app/test-templates")]

    public class TestTemplateController : TestTemplateControllerBase, ITestTemplatesAppService
    {
        public TestTemplateController(ITestTemplatesAppService testTemplatesAppService) : base(testTemplatesAppService)
        {
        }
    }
}