using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace abp.Templates
{
    public abstract class TemplateUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? name { get; set; }
        public int number { get; set; }
        public string? description { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}