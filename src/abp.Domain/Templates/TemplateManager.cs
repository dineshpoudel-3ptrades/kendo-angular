using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace abp.Templates
{
    public abstract class TemplateManagerBase : DomainService
    {
        protected ITemplateRepository _templateRepository;

        public TemplateManagerBase(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public virtual async Task<Template> CreateAsync(
        int number, string? name = null, string? description = null)
        {

            var template = new Template(
             GuidGenerator.Create(),
             number, name, description
             );

            return await _templateRepository.InsertAsync(template);
        }

        public virtual async Task<Template> UpdateAsync(
            Guid id,
            int number, string? name = null, string? description = null, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var template = await _templateRepository.GetAsync(id);

            template.number = number;
            template.name = name;
            template.description = description;

            template.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _templateRepository.UpdateAsync(template);
        }

    }
}