using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using abp.Feedbacks;
using Volo.Abp.Content;
using abp.Shared;

namespace abp.Controllers.Feedbacks
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Feedback")]
    [Route("api/app/feedbacks")]

    public abstract class FeedbackControllerBase : AbpController
    {
        protected IFeedbacksAppService _feedbacksAppService;

        public FeedbackControllerBase(IFeedbacksAppService feedbacksAppService)
        {
            _feedbacksAppService = feedbacksAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<FeedbackDto>> GetListAsync(GetFeedbacksInput input)
        {
            return _feedbacksAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<FeedbackDto> GetAsync(Guid id)
        {
            return _feedbacksAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<FeedbackDto> CreateAsync(FeedbackCreateDto input)
        {
            return _feedbacksAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<FeedbackDto> UpdateAsync(Guid id, FeedbackUpdateDto input)
        {
            return _feedbacksAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _feedbacksAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(FeedbackExcelDownloadDto input)
        {
            return _feedbacksAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<abp.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _feedbacksAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> feedbackIds)
        {
            return _feedbacksAppService.DeleteByIdsAsync(feedbackIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetFeedbacksInput input)
        {
            return _feedbacksAppService.DeleteAllAsync(input);
        }
    }
}