using System;

namespace abp.Feedbacks;

public abstract class FeedbackDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}