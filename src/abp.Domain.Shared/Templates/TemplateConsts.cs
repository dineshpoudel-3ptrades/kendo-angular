namespace abp.Templates
{
    public static class TemplateConsts
    {
        private const string DefaultSorting = "{0}name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Template." : string.Empty);
        }

    }
}