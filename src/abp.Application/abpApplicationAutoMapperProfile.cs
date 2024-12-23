using abp.Parts;
using System;
using abp.Shared;
using Volo.Abp.AutoMapper;
using abp.Feedbacks;
using AutoMapper;

namespace abp;

public class abpApplicationAutoMapperProfile : Profile
{
    public abpApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Feedback, FeedbackDto>();
        CreateMap<Feedback, FeedbackExcelDto>();

        CreateMap<Part, PartDto>();
        CreateMap<Part, PartExcelDto>();
    }
}