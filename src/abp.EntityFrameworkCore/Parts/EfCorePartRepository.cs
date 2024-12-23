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

namespace abp.Parts
{
    public abstract class EfCorePartRepositoryBase : EfCoreRepository<abpDbContext, Part, Guid>
    {
        public EfCorePartRepositoryBase(IDbContextProvider<abpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task DeleteAllAsync(
            string? filterText = null,
                        string? name = null,
            int? numberMin = null,
            int? numberMax = null,
            CancellationToken cancellationToken = default)
        {

            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText, name, numberMin, numberMax);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<Part>> GetListAsync(
            string? filterText = null,
            string? name = null,
            int? numberMin = null,
            int? numberMax = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, numberMin, numberMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PartConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            int? numberMin = null,
            int? numberMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, numberMin, numberMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Part> ApplyFilter(
            IQueryable<Part> query,
            string? filterText = null,
            string? name = null,
            int? numberMin = null,
            int? numberMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.name!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.name.Contains(name))
                    .WhereIf(numberMin.HasValue, e => e.number >= numberMin!.Value)
                    .WhereIf(numberMax.HasValue, e => e.number <= numberMax!.Value);
        }
    }
}