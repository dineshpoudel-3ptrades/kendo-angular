using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace abp.TestTemplates
{
    public partial interface ITestTemplateRepository : IRepository<TestTemplate, Guid>
    {

        Task DeleteAllAsync(
            string? filterText = null,
            string? name = null,
            int? numberMin = null,
            int? numberMax = null,
            string? description = null,
            CancellationToken cancellationToken = default);
        Task<List<TestTemplate>> GetListAsync(
                    string? filterText = null,
                    string? name = null,
                    int? numberMin = null,
                    int? numberMax = null,
                    string? description = null,
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
            string? description = null,
            CancellationToken cancellationToken = default);
    }
}