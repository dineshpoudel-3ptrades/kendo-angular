using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using abp.TestTemplates;
using abp.EntityFrameworkCore;
using Xunit;

namespace abp.EntityFrameworkCore.Domains.TestTemplates
{
    public class TestTemplateRepositoryTests : abpEntityFrameworkCoreTestBase
    {
        private readonly ITestTemplateRepository _testTemplateRepository;

        public TestTemplateRepositoryTests()
        {
            _testTemplateRepository = GetRequiredService<ITestTemplateRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _testTemplateRepository.GetListAsync(
                    name: "988e180d7fd5445c9492e84bc8a7f7c7bde1caebf52c46d1b29cd06e54a48b41d315672b433",
                    description: "b606cac7e8874304a2567a756df3d7fdc45f96e2"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("1359eaa6-fa32-425d-aa6d-4d96db803d80"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _testTemplateRepository.GetCountAsync(
                    name: "2779e236f55f4dc9af698ef4854d557b37cad75406a54c008a08ecfe80",
                    description: "10bbb80dd39f43549087df933564c163f1"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}