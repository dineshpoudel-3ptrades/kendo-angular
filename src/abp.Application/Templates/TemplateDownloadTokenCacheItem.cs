using System;

namespace abp.Templates;

public abstract class TemplateDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}