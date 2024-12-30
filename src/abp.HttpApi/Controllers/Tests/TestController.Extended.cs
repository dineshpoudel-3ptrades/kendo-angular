using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using abp.Tests;

namespace abp.Controllers.Tests
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Test")]
    [Route("api/app/tests")]

    public class TestController : TestControllerBase, ITestsAppService
    {
        public TestController(ITestsAppService testsAppService) : base(testsAppService)
        {
        }
    }
}