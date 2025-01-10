using System;

namespace abp.Templates
{
    public abstract class TemplateExcelDtoBase
    {
        public string? name { get; set; }
        public int number { get; set; }
        public string? description { get; set; }
    }
}