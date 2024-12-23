using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace abp.Parts
{
    public abstract class PartBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? name { get; set; }

        public virtual int number { get; set; }

        protected PartBase()
        {

        }

        public PartBase(Guid id, int number, string? name = null)
        {

            Id = id;
            this.number = number;
            this.name = name;
        }

    }
}