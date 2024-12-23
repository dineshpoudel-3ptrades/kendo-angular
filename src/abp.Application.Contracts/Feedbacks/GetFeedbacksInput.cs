using Volo.Abp.Application.Dtos;
using System;

namespace abp.Feedbacks
{
    public abstract class GetFeedbacksInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? name { get; set; }
        public int? numberMin { get; set; }
        public int? numberMax { get; set; }

        public GetFeedbacksInputBase()
        {

        }
    }
}