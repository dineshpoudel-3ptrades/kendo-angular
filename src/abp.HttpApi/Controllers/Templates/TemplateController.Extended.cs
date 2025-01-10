using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using abp.Templates;

namespace abp.Controllers.Templates
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Template")]
    [Route("api/app/templates")]

    public class TemplateController : TemplateControllerBase, ITemplatesAppService
    {
        public TemplateController(ITemplatesAppService templatesAppService) : base(templatesAppService)
        {
        }
    }
}