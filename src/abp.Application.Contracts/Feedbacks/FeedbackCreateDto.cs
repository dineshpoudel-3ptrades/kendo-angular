using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace abp.Feedbacks
{
    public abstract class FeedbackCreateDtoBase
    {
        public string? name { get; set; }
        public int number { get; set; }
    }
}