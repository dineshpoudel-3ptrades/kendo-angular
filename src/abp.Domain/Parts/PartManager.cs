using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace abp.Parts
{
    public abstract class PartManagerBase : DomainService
    {
        protected IPartRepository _partRepository;

        public PartManagerBase(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public virtual async Task<Part> CreateAsync(
        int number, string? name = null)
        {

            var part = new Part(
             GuidGenerator.Create(),
             number, name
             );

            return await _partRepository.InsertAsync(part);
        }

        public virtual async Task<Part> UpdateAsync(
            Guid id,
            int number, string? name = null, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var part = await _partRepository.GetAsync(id);

            part.number = number;
            part.name = name;

            part.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _partRepository.UpdateAsync(part);
        }

    }
}