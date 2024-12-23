using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using abp.Feedbacks;

namespace abp.Controllers.Feedbacks
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Feedback")]
    [Route("api/app/feedbacks")]

    public class FeedbackController : FeedbackControllerBase, IFeedbacksAppService
    {
        public FeedbackController(IFeedbacksAppService feedbacksAppService) : base(feedbacksAppService)
        {
        }
    }
}