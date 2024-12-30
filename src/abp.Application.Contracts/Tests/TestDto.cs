using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace abp.Tests
{
    public abstract class TestDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? name { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}