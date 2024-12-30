using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace abp.Tests
{
    public abstract class TestCreateDtoBase
    {
        public string? name { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
    }
}