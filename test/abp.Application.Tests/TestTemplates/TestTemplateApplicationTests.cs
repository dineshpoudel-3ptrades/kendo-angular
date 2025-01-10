using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace abp.TestTemplates
{
    public abstract class TestTemplatesAppServiceTests<TStartupModule> : abpApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly ITestTemplatesAppService _testTemplatesAppService;
        private readonly IRepository<TestTemplate, Guid> _testTemplateRepository;

        public TestTemplatesAppServiceTests()
        {
            _testTemplatesAppService = GetRequiredService<ITestTemplatesAppService>();
            _testTemplateRepository = GetRequiredService<IRepository<TestTemplate, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _testTemplatesAppService.GetListAsync(new GetTestTemplatesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("1359eaa6-fa32-425d-aa6d-4d96db803d80")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("8ac95d1e-d114-47bc-9be8-443de84a767c")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _testTemplatesAppService.GetAsync(Guid.Parse("1359eaa6-fa32-425d-aa6d-4d96db803d80"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("1359eaa6-fa32-425d-aa6d-4d96db803d80"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TestTemplateCreateDto
            {
                name = "d2046ec037ea4548a7c93830b9ee2b4",
                number = 1613496579,
                description = "08fd76629dde4f1aa4b875247bda352edc480c26da"
            };

            // Act
            var serviceResult = await _testTemplatesAppService.CreateAsync(input);

            // Assert
            var result = await _testTemplateRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.name.ShouldBe("d2046ec037ea4548a7c93830b9ee2b4");
            result.number.ShouldBe(1613496579);
            result.description.ShouldBe("08fd76629dde4f1aa4b875247bda352edc480c26da");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TestTemplateUpdateDto()
            {
                name = "da58c5bb74334a3aa980ec1dbe807a79f1fc1d61bcad450092cc6440eed00ded928af23c56b942c1bef0be58b7a",
                number = 1366913761,
                description = "ba5c02863e5"
            };

            // Act
            var serviceResult = await _testTemplatesAppService.UpdateAsync(Guid.Parse("1359eaa6-fa32-425d-aa6d-4d96db803d80"), input);

            // Assert
            var result = await _testTemplateRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.name.ShouldBe("da58c5bb74334a3aa980ec1dbe807a79f1fc1d61bcad450092cc6440eed00ded928af23c56b942c1bef0be58b7a");
            result.number.ShouldBe(1366913761);
            result.description.ShouldBe("ba5c02863e5");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _testTemplatesAppService.DeleteAsync(Guid.Parse("1359eaa6-fa32-425d-aa6d-4d96db803d80"));

            // Assert
            var result = await _testTemplateRepository.FindAsync(c => c.Id == Guid.Parse("1359eaa6-fa32-425d-aa6d-4d96db803d80"));

            result.ShouldBeNull();
        }
    }
}