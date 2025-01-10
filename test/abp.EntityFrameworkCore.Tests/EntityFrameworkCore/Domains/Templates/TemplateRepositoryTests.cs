using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using abp.Templates;
using abp.EntityFrameworkCore;
using Xunit;

namespace abp.EntityFrameworkCore.Domains.Templates
{
    public class TemplateRepositoryTests : abpEntityFrameworkCoreTestBase
    {
        private readonly ITemplateRepository _templateRepository;

        public TemplateRepositoryTests()
        {
            _templateRepository = GetRequiredService<ITemplateRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _templateRepository.GetListAsync(
                    name: "bb00e989970a4548a4f8995",
                    description: "be4062a"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d522fb29-5bad-4c8a-ad31-ebc04f55f712"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _templateRepository.GetCountAsync(
                    name: "ddce9a9ed68f4a39851dad9968a9fd3d4a4b9ee0177746cc8bbb774696",
                    description: "2bfe1a06791b44baaac7955bdb2e09fa6934f52e963844dba2fe9ab6780ae6c65e52fc2fe6b14a159ce31"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}