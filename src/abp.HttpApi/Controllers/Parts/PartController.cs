using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using abp.Parts;
using Volo.Abp.Content;
using abp.Shared;

namespace abp.Controllers.Parts
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Part")]
    [Route("api/app/parts")]

    public abstract class PartControllerBase : AbpController
    {
        protected IPartsAppService _partsAppService;

        public PartControllerBase(IPartsAppService partsAppService)
        {
            _partsAppService = partsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<PartDto>> GetListAsync(GetPartsInput input)
        {
            return _partsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PartDto> GetAsync(Guid id)
        {
            return _partsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<PartDto> CreateAsync(PartCreateDto input)
        {
            return _partsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<PartDto> UpdateAsync(Guid id, PartUpdateDto input)
        {
            return _partsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _partsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(PartExcelDownloadDto input)
        {
            return _partsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _partsAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> partIds)
        {
            return _partsAppService.DeleteByIdsAsync(partIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetPartsInput input)
        {
            return _partsAppService.DeleteAllAsync(input);
        }
    }
}