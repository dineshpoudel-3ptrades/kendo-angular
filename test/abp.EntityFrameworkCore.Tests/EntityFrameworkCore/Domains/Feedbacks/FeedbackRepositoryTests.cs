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
                    name: "bc6f453113db4e65bea53979107dc6eb9b1272be8cf74"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("8a1ce911-301c-491f-9c88-d615260ab197"));
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
                    name: "c36323f0ecfc40ebaeb99c3458cfe2963ff337867b4c494f9c45a"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}