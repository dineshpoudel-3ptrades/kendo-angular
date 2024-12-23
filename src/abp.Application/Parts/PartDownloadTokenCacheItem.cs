using System;

namespace abp.Parts;

public abstract class PartDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}