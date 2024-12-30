using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using abp.EntityFrameworkCore;

namespace abp.Tests
{
    public abstract class EfCoreTestRepositoryBase : EfCoreRepository<abpDbContext, Test, Guid>
    {
        public EfCoreTestRepositoryBase(IDbContextProvider<abpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task DeleteAllAsync(
            string? filterText = null,
                        string? name = null,
            int? ageMin = null,
            int? ageMax = null,
            DateTime? dOBMin = null,
            DateTime? dOBMax = null,
            CancellationToken cancellationToken = default)
        {

            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText, name, ageMin, ageMax, dOBMin, dOBMax);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<Test>> GetListAsync(
            string? filterText = null,
            string? name = null,
            int? ageMin = null,
            int? ageMax = null,
            DateTime? dOBMin = null,
            DateTime? dOBMax = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, ageMin, ageMax, dOBMin, dOBMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            int? ageMin = null,
            int? ageMax = null,
            DateTime? dOBMin = null,
            DateTime? dOBMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, ageMin, ageMax, dOBMin, dOBMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Test> ApplyFilter(
            IQueryable<Test> query,
            string? filterText = null,
            string? name = null,
            int? ageMin = null,
            int? ageMax = null,
            DateTime? dOBMin = null,
            DateTime? dOBMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.name!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.name.Contains(name))
                    .WhereIf(ageMin.HasValue, e => e.Age >= ageMin!.Value)
                    .WhereIf(ageMax.HasValue, e => e.Age <= ageMax!.Value)
                    .WhereIf(dOBMin.HasValue, e => e.DOB >= dOBMin!.Value)
                    .WhereIf(dOBMax.HasValue, e => e.DOB <= dOBMax!.Value);
        }
    }
}