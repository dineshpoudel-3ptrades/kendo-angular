using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace abp.Parts
{
    public abstract class PartCreateDtoBase
    {
        public string? name { get; set; }
        public int number { get; set; }
    }
}