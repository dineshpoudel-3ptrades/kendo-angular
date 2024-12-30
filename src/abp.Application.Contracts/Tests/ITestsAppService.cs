using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using abp.Shared;

namespace abp.Tests
{
    public partial interface ITestsAppService : IApplicationService
    {

        Task<PagedResultDto<TestDto>> GetListAsync(GetTestsInput input);

        Task<TestDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<TestDto> CreateAsync(TestCreateDto input);

        Task<TestDto> UpdateAsync(Guid id, TestUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> testIds);

        Task DeleteAllAsync(GetTestsInput input);
        Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}