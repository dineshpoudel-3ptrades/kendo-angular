using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using abp.Feedbacks;
using abp.EntityFrameworkCore;
using Xunit;

namespace abp.EntityFrameworkCore.Domains.Feedbacks
{
    public class FeedbackRepositoryTests : abpEntityFrameworkCoreTestBase
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackRepositoryTests()
        {
            _feedbackRepository = GetRequiredService<IFeedbackRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _feedbackRepository.GetListAsync(
                    name: "6d35dc7ae7fd4b88996c73396e3771453eea209b5119422cbfede6f4aa92794831a56029ea8e455b9d"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("823af619-ce45-4b57-a37c-a78e18ad7210"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _feedbackRepository.GetCountAsync(
                    name: "f37f36a269ad405fbc30c88ea8bb212ada5fbd16bdd0460c89b7b5b7af2b86c58c60dd16450f48d2af734ad54798d055ad3"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}