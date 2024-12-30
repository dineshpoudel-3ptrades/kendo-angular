using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using abp.Tests;
using abp.EntityFrameworkCore;
using Xunit;

namespace abp.EntityFrameworkCore.Domains.Tests
{
    public class TestRepositoryTests : abpEntityFrameworkCoreTestBase
    {
        private readonly ITestRepository _testRepository;

        public TestRepositoryTests()
        {
            _testRepository = GetRequiredService<ITestRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _testRepository.GetListAsync(
                    name: "024032a37c264ae9a5cd01427d3b7a09dbc2f36d9d9143a5926b039937cac0997bdb5388a6f34fb3b188bb"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("41effc9f-748a-4466-a7e1-5a00a71cf1e7"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _testRepository.GetCountAsync(
                    name: "1505e3df9eec470bbac79c907e1e765d63a96f014f1446c8a91e1cd4857ded3e1ba56bdcec514a8cbd5dea2a8c2950"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}