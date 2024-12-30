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
    public class TestsAppService : TestsAppServiceBase, ITestsAppService
    {
        //<suite-custom-code-autogenerated>
        public TestsAppService(ITestRepository testRepository, TestManager testManager, IDistributedCache<TestDownloadTokenCacheItem, string> downloadTokenCache)
            : base(testRepository, testManager, downloadTokenCache)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}