using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace abp.TestTemplates
{
    public abstract class TestTemplateDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? name { get; set; }
        public int number { get; set; }
        public string? description { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}