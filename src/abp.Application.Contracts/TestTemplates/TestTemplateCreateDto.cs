using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace abp.TestTemplates
{
    public abstract class TestTemplateCreateDtoBase
    {
        public string? name { get; set; }
        public int number { get; set; }
        public string? description { get; set; }
    }
}