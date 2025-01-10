using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace abp.Templates
{
    public abstract class TemplatesAppServiceTests<TStartupModule> : abpApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly ITemplatesAppService _templatesAppService;
        private readonly IRepository<Template, Guid> _templateRepository;

        public TemplatesAppServiceTests()
        {
            _templatesAppService = GetRequiredService<ITemplatesAppService>();
            _templateRepository = GetRequiredService<IRepository<Template, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _templatesAppService.GetListAsync(new GetTemplatesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("d522fb29-5bad-4c8a-ad31-ebc04f55f712")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("a4d2bf10-b775-4c3d-836c-404667ba5da2")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _templatesAppService.GetAsync(Guid.Parse("d522fb29-5bad-4c8a-ad31-ebc04f55f712"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d522fb29-5bad-4c8a-ad31-ebc04f55f712"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TemplateCreateDto
            {
                name = "6b5b908143e94234a48da8181b55ca415b21815637b94e738",
                number = 106173074,
                description = "25b4dffbec704b69a958278accfb2cc3c8"
            };

            // Act
            var serviceResult = await _templatesAppService.CreateAsync(input);

            // Assert
            var result = await _templateRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.name.ShouldBe("6b5b908143e94234a48da8181b55ca415b21815637b94e738");
            result.number.ShouldBe(106173074);
            result.description.ShouldBe("25b4dffbec704b69a958278accfb2cc3c8");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TemplateUpdateDto()
            {
                name = "b48b8503b5ce401e938edec51b2ab98966c9e72ab28c4e60811f26470a7970c8f2b852218357",
                number = 1421185509,
                description = "011f49e3317441d689f21"
            };

            // Act
            var serviceResult = await _templatesAppService.UpdateAsync(Guid.Parse("d522fb29-5bad-4c8a-ad31-ebc04f55f712"), input);

            // Assert
            var result = await _templateRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.name.ShouldBe("b48b8503b5ce401e938edec51b2ab98966c9e72ab28c4e60811f26470a7970c8f2b852218357");
            result.number.ShouldBe(1421185509);
            result.description.ShouldBe("011f49e3317441d689f21");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _templatesAppService.DeleteAsync(Guid.Parse("d522fb29-5bad-4c8a-ad31-ebc04f55f712"));

            // Assert
            var result = await _templateRepository.FindAsync(c => c.Id == Guid.Parse("d522fb29-5bad-4c8a-ad31-ebc04f55f712"));

            result.ShouldBeNull();
        }
    }
}