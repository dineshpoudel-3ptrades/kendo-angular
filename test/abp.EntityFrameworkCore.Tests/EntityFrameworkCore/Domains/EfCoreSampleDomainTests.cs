using abp.Samples;
using Xunit;

namespace abp.EntityFrameworkCore.Domains;

[Collection(abpTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<abpEntityFrameworkCoreTestModule>
{

}
