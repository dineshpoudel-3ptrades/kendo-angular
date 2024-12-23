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
    public class Feedback : FeedbackBase
    {
        //<suite-custom-code-autogenerated>
        protected Feedback()
        {

        }

        public Feedback(Guid id, int number, string? name = null)
            : base(id, number, name)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}