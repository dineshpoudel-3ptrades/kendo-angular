using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace abp.Feedbacks
{
    public abstract class FeedbackUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? name { get; set; }
        [Range(FeedbackConsts.numberMinLength, FeedbackConsts.numberMaxLength)]
        public int number { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}