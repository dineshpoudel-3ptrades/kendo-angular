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
                id: Guid.Parse("8a1ce911-301c-491f-9c88-d615260ab197"),
                name: "bc6f453113db4e65bea53979107dc6eb9b1272be8cf74",
                number: 259
            ));

            await _feedbackRepository.InsertAsync(new Feedback
            (
                id: Guid.Parse("99141367-17b1-4312-bd19-bc8848d21095"),
                name: "c36323f0ecfc40ebaeb99c3458cfe2963ff337867b4c494f9c45a",
                number: 602
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}