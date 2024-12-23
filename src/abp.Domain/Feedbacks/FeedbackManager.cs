using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace abp.Feedbacks
{
    public abstract class FeedbackManagerBase : DomainService
    {
        protected IFeedbackRepository _feedbackRepository;

        public FeedbackManagerBase(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public virtual async Task<Feedback> CreateAsync(
        int number, string? name = null)
        {

            var feedback = new Feedback(
             GuidGenerator.Create(),
             number, name
             );

            return await _feedbackRepository.InsertAsync(feedback);
        }

        public virtual async Task<Feedback> UpdateAsync(
            Guid id,
            int number, string? name = null, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var feedback = await _feedbackRepository.GetAsync(id);

            feedback.number = number;
            feedback.name = name;

            feedback.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _feedbackRepository.UpdateAsync(feedback);
        }

    }
}