using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using abp.Tests;

namespace abp.Tests
{
    public class TestsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITestRepository _testRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TestsDataSeedContributor(ITestRepository testRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _testRepository = testRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _testRepository.InsertAsync(new Test
            (
                id: Guid.Parse("41effc9f-748a-4466-a7e1-5a00a71cf1e7"),
                name: "024032a37c264ae9a5cd01427d3b7a09dbc2f36d9d9143a5926b039937cac0997bdb5388a6f34fb3b188bb",
                age: 87954079,
                dOB: new DateTime(2012, 8, 27)
            ));

            await _testRepository.InsertAsync(new Test
            (
                id: Guid.Parse("6f62f120-e987-47a0-9b28-5029dfcfc846"),
                name: "1505e3df9eec470bbac79c907e1e765d63a96f014f1446c8a91e1cd4857ded3e1ba56bdcec514a8cbd5dea2a8c2950",
                age: 801225956,
                dOB: new DateTime(2010, 9, 24)
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}