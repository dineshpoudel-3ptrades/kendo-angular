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
    public class EfCoreTestRepository : EfCoreTestRepositoryBase, ITestRepository
    {
        public EfCoreTestRepository(IDbContextProvider<abpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}