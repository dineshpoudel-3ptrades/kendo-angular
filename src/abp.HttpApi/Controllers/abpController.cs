﻿using abp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace abp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class abpController : AbpControllerBase
{
    protected abpController()
    {
        LocalizationResource = typeof(abpResource);
    }
}
