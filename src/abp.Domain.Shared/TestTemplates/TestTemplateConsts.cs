namespace abp.TestTemplates
{
    public static class TestTemplateConsts
    {
        private const string DefaultSorting = "{0}name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "TestTemplate." : string.Empty);
        }

    }
}