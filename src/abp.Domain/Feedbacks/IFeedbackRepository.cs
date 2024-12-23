using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace abp.Feedbacks
{
    public partial interface IFeedbackRepository : IRepository<Feedback, Guid>
    {

        Task DeleteAllAsync(
            string? filterText = null,
            string? name = null,
            int? numberMin = null,
            int? numberMax = null,
            CancellationToken cancellationToken = default);
        Task<List<Feedback>> GetListAsync(
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