using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace abp.Feedbacks
{
    public abstract class FeedbacksAppServiceTests<TStartupModule> : abpApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IFeedbacksAppService _feedbacksAppService;
        private readonly IRepository<Feedback, Guid> _feedbackRepository;

        public FeedbacksAppServiceTests()
        {
            _feedbacksAppService = GetRequiredService<IFeedbacksAppService>();
            _feedbackRepository = GetRequiredService<IRepository<Feedback, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _feedbacksAppService.GetListAsync(new GetFeedbacksInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("8a1ce911-301c-491f-9c88-d615260ab197")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("99141367-17b1-4312-bd19-bc8848d21095")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _feedbacksAppService.GetAsync(Guid.Parse("8a1ce911-301c-491f-9c88-d615260ab197"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("8a1ce911-301c-491f-9c88-d615260ab197"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new FeedbackCreateDto
            {
                name = "43c69dab465d4acbb11a5d07dca398ba0c5f2c906080423ab88486d4d2b93be",
                number = 792
            };

            // Act
            var serviceResult = await _feedbacksAppService.CreateAsync(input);

            // Assert
            var result = await _feedbackRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.name.ShouldBe("43c69dab465d4acbb11a5d07dca398ba0c5f2c906080423ab88486d4d2b93be");
            result.number.ShouldBe(792);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new FeedbackUpdateDto()
            {
                name = "bc9862e0bd494a058085971080ce72c0755d8cc006ec4a15a7990b074adfa2885858dce7d1674e17",
                number = 432
            };

            // Act
            var serviceResult = await _feedbacksAppService.UpdateAsync(Guid.Parse("8a1ce911-301c-491f-9c88-d615260ab197"), input);

            // Assert
            var result = await _feedbackRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.name.ShouldBe("bc9862e0bd494a058085971080ce72c0755d8cc006ec4a15a7990b074adfa2885858dce7d1674e17");
            result.number.ShouldBe(432);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _feedbacksAppService.DeleteAsync(Guid.Parse("8a1ce911-301c-491f-9c88-d615260ab197"));

            // Assert
            var result = await _feedbackRepository.FindAsync(c => c.Id == Guid.Parse("8a1ce911-301c-491f-9c88-d615260ab197"));

            result.ShouldBeNull();
        }
    }
}