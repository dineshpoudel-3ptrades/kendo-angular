using Microsoft.Extensions.Localization;
using abp.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace abp;

[Dependency(ReplaceServices = true)]
public class abpBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<abpResource> _localizer;

    public abpBrandingProvider(IStringLocalizer<abpResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
