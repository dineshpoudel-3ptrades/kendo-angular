using abp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace abp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(abpEntityFrameworkCoreModule),
    typeof(abpApplicationContractsModule)
)]
public class abpDbMigratorModule : AbpModule
{
}
