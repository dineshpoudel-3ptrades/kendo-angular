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
            result.Items.Any(x => x.Id == Guid.Parse("823af619-ce45-4b57-a37c-a78e18ad7210")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("c1a84ce7-8798-4005-9077-6f83685b82de")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _feedbacksAppService.GetAsync(Guid.Parse("823af619-ce45-4b57-a37c-a78e18ad7210"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("823af619-ce45-4b57-a37c-a78e18ad7210"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new FeedbackCreateDto
            {
                name = "5aedba9e",
                number = 39980910
            };

            // Act
            var serviceResult = await _feedbacksAppService.CreateAsync(input);

            // Assert
            var result = await _feedbackRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.name.ShouldBe("5aedba9e");
            result.number.ShouldBe(39980910);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new FeedbackUpdateDto()
            {
                name = "a9faa567052e486c97798cb5b",
                number = 824487521
            };

            // Act
            var serviceResult = await _feedbacksAppService.UpdateAsync(Guid.Parse("823af619-ce45-4b57-a37c-a78e18ad7210"), input);

            // Assert
            var result = await _feedbackRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.name.ShouldBe("a9faa567052e486c97798cb5b");
            result.number.ShouldBe(824487521);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _feedbacksAppService.DeleteAsync(Guid.Parse("823af619-ce45-4b57-a37c-a78e18ad7210"));

            // Assert
            var result = await _feedbackRepository.FindAsync(c => c.Id == Guid.Parse("823af619-ce45-4b57-a37c-a78e18ad7210"));

            result.ShouldBeNull();
        }
    }
}