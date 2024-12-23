using Volo.Abp.Modularity;

namespace abp;

/* Inherit from this class for your domain layer tests. */
public abstract class abpDomainTestBase<TStartupModule> : abpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
