using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace abp.Feedbacks
{
    public abstract class FeedbackBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? name { get; set; }

        public virtual int number { get; set; }

        protected FeedbackBase()
        {

        }

        public FeedbackBase(Guid id, int number, string? name = null)
        {

            Id = id;
            if (number < FeedbackConsts.numberMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(number), number, "The value of 'number' cannot be lower than " + FeedbackConsts.numberMinLength);
            }

            if (number > FeedbackConsts.numberMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(number), number, "The value of 'number' cannot be greater than " + FeedbackConsts.numberMaxLength);
            }

            this.number = number;
            this.name = name;
        }

    }
}