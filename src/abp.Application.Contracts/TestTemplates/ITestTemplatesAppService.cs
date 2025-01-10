using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using abp.Shared;

namespace abp.TestTemplates
{
    public partial interface ITestTemplatesAppService : IApplicationService
    {

        Task<PagedResultDto<TestTemplateDto>> GetListAsync(GetTestTemplatesInput input);

        Task<TestTemplateDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<TestTemplateDto> CreateAsync(TestTemplateCreateDto input);

        Task<TestTemplateDto> UpdateAsync(Guid id, TestTemplateUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestTemplateExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> testtemplateIds);

        Task DeleteAllAsync(GetTestTemplatesInput input);
        Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}