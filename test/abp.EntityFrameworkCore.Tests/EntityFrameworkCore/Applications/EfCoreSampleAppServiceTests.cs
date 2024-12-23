using abp.Samples;
using Xunit;

namespace abp.EntityFrameworkCore.Applications;

[Collection(abpTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<abpEntityFrameworkCoreTestModule>
{

}
