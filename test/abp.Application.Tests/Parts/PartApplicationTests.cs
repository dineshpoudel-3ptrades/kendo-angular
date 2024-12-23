using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace abp.Parts
{
    public abstract class PartsAppServiceTests<TStartupModule> : abpApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IPartsAppService _partsAppService;
        private readonly IRepository<Part, Guid> _partRepository;

        public PartsAppServiceTests()
        {
            _partsAppService = GetRequiredService<IPartsAppService>();
            _partRepository = GetRequiredService<IRepository<Part, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _partsAppService.GetListAsync(new GetPartsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("6d83767d-76d1-4ba2-afb6-b147db00adf0")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("0a7126b1-ecbe-41d0-ab25-46bc97feebcb")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _partsAppService.GetAsync(Guid.Parse("6d83767d-76d1-4ba2-afb6-b147db00adf0"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6d83767d-76d1-4ba2-afb6-b147db00adf0"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PartCreateDto
            {
                name = "85d9d339917e4f90bcd81a6e610269eeeed962d61edb4e258e4da3bb87e4d225680abd76bdf741aca39450",
                number = 1131773198
            };

            // Act
            var serviceResult = await _partsAppService.CreateAsync(input);

            // Assert
            var result = await _partRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.name.ShouldBe("85d9d339917e4f90bcd81a6e610269eeeed962d61edb4e258e4da3bb87e4d225680abd76bdf741aca39450");
            result.number.ShouldBe(1131773198);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PartUpdateDto()
            {
                name = "7fa112ece33248c2842ee77c4175d9b61370a18db9194811b2f60d4bfe467be92ee5b96a03134773b7cb39",
                number = 1724213178
            };

            // Act
            var serviceResult = await _partsAppService.UpdateAsync(Guid.Parse("6d83767d-76d1-4ba2-afb6-b147db00adf0"), input);

            // Assert
            var result = await _partRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.name.ShouldBe("7fa112ece33248c2842ee77c4175d9b61370a18db9194811b2f60d4bfe467be92ee5b96a03134773b7cb39");
            result.number.ShouldBe(1724213178);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _partsAppService.DeleteAsync(Guid.Parse("6d83767d-76d1-4ba2-afb6-b147db00adf0"));

            // Assert
            var result = await _partRepository.FindAsync(c => c.Id == Guid.Parse("6d83767d-76d1-4ba2-afb6-b147db00adf0"));

            result.ShouldBeNull();
        }
    }
}