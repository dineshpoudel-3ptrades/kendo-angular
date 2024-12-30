using Volo.Abp.Application.Dtos;
using System;

namespace abp.Tests
{
    public abstract class GetTestsInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? name { get; set; }
        public int? AgeMin { get; set; }
        public int? AgeMax { get; set; }
        public DateTime? DOBMin { get; set; }
        public DateTime? DOBMax { get; set; }

        public GetTestsInputBase()
        {

        }
    }
}