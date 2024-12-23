using Xunit;

namespace abp.EntityFrameworkCore;

[CollectionDefinition(abpTestConsts.CollectionDefinitionName)]
public class abpEntityFrameworkCoreCollection : ICollectionFixture<abpEntityFrameworkCoreFixture>
{

}
