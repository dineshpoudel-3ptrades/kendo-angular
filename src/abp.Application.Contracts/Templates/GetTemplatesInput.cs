using Volo.Abp.Application.Dtos;
using System;

namespace abp.Templates
{
    public abstract class GetTemplatesInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? name { get; set; }
        public int? numberMin { get; set; }
        public int? numberMax { get; set; }
        public string? description { get; set; }

        public GetTemplatesInputBase()
        {

        }
    }
}