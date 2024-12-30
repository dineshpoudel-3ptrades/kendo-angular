using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace abp.Tests
{
    public abstract class TestUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? name { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}