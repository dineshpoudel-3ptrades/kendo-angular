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
    public class TemplatesAppService : TemplatesAppServiceBase, ITemplatesAppService
    {
        //<suite-custom-code-autogenerated>
        public TemplatesAppService(ITemplateRepository templateRepository, TemplateManager templateManager, IDistributedCache<TemplateDownloadTokenCacheItem, string> downloadTokenCache)
            : base(templateRepository, templateManager, downloadTokenCache)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}