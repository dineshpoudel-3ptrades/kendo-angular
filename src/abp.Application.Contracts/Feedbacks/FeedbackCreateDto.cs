using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace abp.Feedbacks
{
    public abstract class FeedbackCreateDtoBase
    {
        public string? name { get; set; }
        [Range(FeedbackConsts.numberMinLength, FeedbackConsts.numberMaxLength)]
        public int number { get; set; }
    }
}