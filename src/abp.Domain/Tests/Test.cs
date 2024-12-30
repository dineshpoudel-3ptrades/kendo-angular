using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace abp.Tests
{
    public abstract class TestBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? name { get; set; }

        public virtual int Age { get; set; }

        public virtual DateTime DOB { get; set; }

        protected TestBase()
        {

        }

        public TestBase(Guid id, int age, DateTime dOB, string? name = null)
        {

            Id = id;
            Age = age;
            DOB = dOB;
            this.name = name;
        }

    }
}