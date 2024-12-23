using Volo.Abp.Modularity;

namespace abp;

[DependsOn(
    typeof(abpDomainModule),
    typeof(abpTestBaseModule)
)]
public class abpDomainTestModule : AbpModule
{

}
