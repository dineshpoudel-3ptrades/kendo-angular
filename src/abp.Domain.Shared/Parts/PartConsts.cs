namespace abp.Parts
{
    public static class PartConsts
    {
        private const string DefaultSorting = "{0}name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Part." : string.Empty);
        }

    }
}