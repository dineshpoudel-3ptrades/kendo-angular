using abp.TestTemplates;
using abp.Templates;
using abp.Tests;
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

        CreateMap<Test, TestDto>();
        CreateMap<Test, TestExcelDto>();

        CreateMap<Template, TemplateDto>();
        CreateMap<Template, TemplateExcelDto>();

        CreateMap<TestTemplate, TestTemplateDto>();
        CreateMap<TestTemplate, TestTemplateExcelDto>();
    }
}