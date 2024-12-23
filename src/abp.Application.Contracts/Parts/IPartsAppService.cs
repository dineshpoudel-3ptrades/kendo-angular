using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using abp.Shared;

namespace abp.Parts
{
    public partial interface IPartsAppService : IApplicationService
    {

        Task<PagedResultDto<PartDto>> GetListAsync(GetPartsInput input);

        Task<PartDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<PartDto> CreateAsync(PartCreateDto input);

        Task<PartDto> UpdateAsync(Guid id, PartUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(PartExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> partIds);

        Task DeleteAllAsync(GetPartsInput input);
        Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}