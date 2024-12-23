using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using abp.Shared;

namespace abp.Feedbacks
{
    public partial interface IFeedbacksAppService : IApplicationService
    {

        Task<PagedResultDto<FeedbackDto>> GetListAsync(GetFeedbacksInput input);

        Task<FeedbackDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<FeedbackDto> CreateAsync(FeedbackCreateDto input);

        Task<FeedbackDto> UpdateAsync(Guid id, FeedbackUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(FeedbackExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> feedbackIds);

        Task DeleteAllAsync(GetFeedbacksInput input);
        Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}