using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace abp.Tests
{
    public abstract class TestManagerBase : DomainService
    {
        protected ITestRepository _testRepository;

        public TestManagerBase(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public virtual async Task<Test> CreateAsync(
        int age, DateTime dOB, string? name = null)
        {
            Check.NotNull(dOB, nameof(dOB));

            var test = new Test(
             GuidGenerator.Create(),
             age, dOB, name
             );

            return await _testRepository.InsertAsync(test);
        }

        public virtual async Task<Test> UpdateAsync(
            Guid id,
            int age, DateTime dOB, string? name = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(dOB, nameof(dOB));

            var test = await _testRepository.GetAsync(id);

            test.Age = age;
            test.DOB = dOB;
            test.name = name;

            test.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _testRepository.UpdateAsync(test);
        }

    }
}