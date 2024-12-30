using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace abp.Tests
{
    public partial interface ITestRepository : IRepository<Test, Guid>
    {

        Task DeleteAllAsync(
            string? filterText = null,
            string? name = null,
            int? ageMin = null,
            int? ageMax = null,
            DateTime? dOBMin = null,
            DateTime? dOBMax = null,
            CancellationToken cancellationToken = default);
        Task<List<Test>> GetListAsync(
                    string? filterText = null,
                    string? name = null,
                    int? ageMin = null,
                    int? ageMax = null,
                    DateTime? dOBMin = null,
                    DateTime? dOBMax = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            int? ageMin = null,
            int? ageMax = null,
            DateTime? dOBMin = null,
            DateTime? dOBMax = null,
            CancellationToken cancellationToken = default);
    }
}