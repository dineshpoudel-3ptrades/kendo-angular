namespace abp.Tests
{
    public static class TestConsts
    {
        private const string DefaultSorting = "{0}name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Test." : string.Empty);
        }

    }
}