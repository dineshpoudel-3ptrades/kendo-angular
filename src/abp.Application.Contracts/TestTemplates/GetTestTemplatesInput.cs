using Volo.Abp.Application.Dtos;
using System;

namespace abp.TestTemplates
{
    public abstract class GetTestTemplatesInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? name { get; set; }
        public int? numberMin { get; set; }
        public int? numberMax { get; set; }
        public string? description { get; set; }

        public GetTestTemplatesInputBase()
        {

        }
    }
}