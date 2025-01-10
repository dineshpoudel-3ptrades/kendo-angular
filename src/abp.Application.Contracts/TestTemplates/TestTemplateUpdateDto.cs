using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace abp.TestTemplates
{
    public abstract class TestTemplateUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? name { get; set; }
        public int number { get; set; }
        public string? description { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}