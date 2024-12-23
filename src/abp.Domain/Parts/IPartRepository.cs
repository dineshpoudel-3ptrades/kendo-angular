using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace abp.Parts
{
    public partial interface IPartRepository : IRepository<Part, Guid>
    {

        Task DeleteAllAsync(
            string? filterText = null,
            string? name = null,
            int? numberMin = null,
            int? numberMax = null,
            CancellationToken cancellationToken = default);
        Task<List<Part>> GetListAsync(
                    string? filterText = null,
                    string? name = null,
                    int? numberMin = null,
                    int? numberMax = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            int? numberMin = null,
            int? numberMax = null,
            CancellationToken cancellationToken = default);
    }
}