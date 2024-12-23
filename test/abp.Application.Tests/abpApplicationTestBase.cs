using Volo.Abp.Modularity;

namespace abp;

public abstract class abpApplicationTestBase<TStartupModule> : abpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
