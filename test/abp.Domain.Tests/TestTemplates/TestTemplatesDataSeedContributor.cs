using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using abp.TestTemplates;

namespace abp.TestTemplates
{
    public class TestTemplatesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITestTemplateRepository _testTemplateRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TestTemplatesDataSeedContributor(ITestTemplateRepository testTemplateRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _testTemplateRepository = testTemplateRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _testTemplateRepository.InsertAsync(new TestTemplate
            (
                id: Guid.Parse("1359eaa6-fa32-425d-aa6d-4d96db803d80"),
                name: "988e180d7fd5445c9492e84bc8a7f7c7bde1caebf52c46d1b29cd06e54a48b41d315672b433",
                number: 264368739,
                description: "b606cac7e8874304a2567a756df3d7fdc45f96e2"
            ));

            await _testTemplateRepository.InsertAsync(new TestTemplate
            (
                id: Guid.Parse("8ac95d1e-d114-47bc-9be8-443de84a767c"),
                name: "2779e236f55f4dc9af698ef4854d557b37cad75406a54c008a08ecfe80",
                number: 747891219,
                description: "10bbb80dd39f43549087df933564c163f1"
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}