using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using abp.Feedbacks;

namespace abp.Feedbacks
{
    public class FeedbacksDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public FeedbacksDataSeedContributor(IFeedbackRepository feedbackRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _feedbackRepository = feedbackRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _feedbackRepository.InsertAsync(new Feedback
            (
                id: Guid.Parse("823af619-ce45-4b57-a37c-a78e18ad7210"),
                name: "6d35dc7ae7fd4b88996c73396e3771453eea209b5119422cbfede6f4aa92794831a56029ea8e455b9d",
                number: 655267429
            ));

            await _feedbackRepository.InsertAsync(new Feedback
            (
                id: Guid.Parse("c1a84ce7-8798-4005-9077-6f83685b82de"),
                name: "f37f36a269ad405fbc30c88ea8bb212ada5fbd16bdd0460c89b7b5b7af2b86c58c60dd16450f48d2af734ad54798d055ad3",
                number: 375243234
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}