using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using abp.Shared;

namespace abp.Templates
{
    public partial interface ITemplatesAppService : IApplicationService
    {

        Task<PagedResultDto<TemplateDto>> GetListAsync(GetTemplatesInput input);

        Task<TemplateDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<TemplateDto> CreateAsync(TemplateCreateDto input);

        Task<TemplateDto> UpdateAsync(Guid id, TemplateUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(TemplateExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> templateIds);

        Task DeleteAllAsync(GetTemplatesInput input);
        Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}