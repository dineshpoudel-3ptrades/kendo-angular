using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace abp.Templates
{
    public abstract class TemplateBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? name { get; set; }

        public virtual int number { get; set; }

        [CanBeNull]
        public virtual string? description { get; set; }

        protected TemplateBase()
        {

        }

        public TemplateBase(Guid id, int number, string? name = null, string? description = null)
        {

            Id = id;
            this.number = number;
            this.name = name;
            this.description = description;
        }

    }
}