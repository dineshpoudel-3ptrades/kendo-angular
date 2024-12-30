using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace abp.Tests
{
    public abstract class TestsAppServiceTests<TStartupModule> : abpApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly ITestsAppService _testsAppService;
        private readonly IRepository<Test, Guid> _testRepository;

        public TestsAppServiceTests()
        {
            _testsAppService = GetRequiredService<ITestsAppService>();
            _testRepository = GetRequiredService<IRepository<Test, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _testsAppService.GetListAsync(new GetTestsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("41effc9f-748a-4466-a7e1-5a00a71cf1e7")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("6f62f120-e987-47a0-9b28-5029dfcfc846")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _testsAppService.GetAsync(Guid.Parse("41effc9f-748a-4466-a7e1-5a00a71cf1e7"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("41effc9f-748a-4466-a7e1-5a00a71cf1e7"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TestCreateDto
            {
                name = "89c96c671a114fd19a6c7fdd0ff83907243dedff59844d4ab7899d1ce2b9f2f328eccb222ac7",
                Age = 2023271090,
                DOB = new DateTime(2023, 1, 19)
            };

            // Act
            var serviceResult = await _testsAppService.CreateAsync(input);

            // Assert
            var result = await _testRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.name.ShouldBe("89c96c671a114fd19a6c7fdd0ff83907243dedff59844d4ab7899d1ce2b9f2f328eccb222ac7");
            result.Age.ShouldBe(2023271090);
            result.DOB.ShouldBe(new DateTime(2023, 1, 19));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TestUpdateDto()
            {
                name = "1ef8bf154a4e4d9ba8959a4fbd21f2b5d1690d00bf7a4515b72ed",
                Age = 2118618625,
                DOB = new DateTime(2003, 4, 14)
            };

            // Act
            var serviceResult = await _testsAppService.UpdateAsync(Guid.Parse("41effc9f-748a-4466-a7e1-5a00a71cf1e7"), input);

            // Assert
            var result = await _testRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.name.ShouldBe("1ef8bf154a4e4d9ba8959a4fbd21f2b5d1690d00bf7a4515b72ed");
            result.Age.ShouldBe(2118618625);
            result.DOB.ShouldBe(new DateTime(2003, 4, 14));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _testsAppService.DeleteAsync(Guid.Parse("41effc9f-748a-4466-a7e1-5a00a71cf1e7"));

            // Assert
            var result = await _testRepository.FindAsync(c => c.Id == Guid.Parse("41effc9f-748a-4466-a7e1-5a00a71cf1e7"));

            result.ShouldBeNull();
        }
    }
}