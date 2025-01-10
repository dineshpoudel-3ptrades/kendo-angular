using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace abp.TestTemplates
{
    public abstract class TestTemplateManagerBase : DomainService
    {
        protected ITestTemplateRepository _testTemplateRepository;

        public TestTemplateManagerBase(ITestTemplateRepository testTemplateRepository)
        {
            _testTemplateRepository = testTemplateRepository;
        }

        public virtual async Task<TestTemplate> CreateAsync(
        int number, string? name = null, string? description = null)
        {

            var testTemplate = new TestTemplate(
             GuidGenerator.Create(),
             number, name, description
             );

            return await _testTemplateRepository.InsertAsync(testTemplate);
        }

        public virtual async Task<TestTemplate> UpdateAsync(
            Guid id,
            int number, string? name = null, string? description = null, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var testTemplate = await _testTemplateRepository.GetAsync(id);

            testTemplate.number = number;
            testTemplate.name = name;
            testTemplate.description = description;

            testTemplate.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _testTemplateRepository.UpdateAsync(testTemplate);
        }

    }
}