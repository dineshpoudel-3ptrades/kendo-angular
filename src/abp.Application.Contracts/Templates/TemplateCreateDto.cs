using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace abp.Templates
{
    public abstract class TemplateCreateDtoBase
    {
        public string? name { get; set; }
        public int number { get; set; }
        public string? description { get; set; }
    }
}