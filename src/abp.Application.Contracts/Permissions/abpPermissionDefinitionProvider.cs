using abp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace abp.Permissions;

public class abpPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(abpPermissions.GroupName);

        myGroup.AddPermission(abpPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(abpPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(abpPermissions.MyPermission1, L("Permission:MyPermission1"));

        var feedbackPermission = myGroup.AddPermission(abpPermissions.Feedbacks.Default, L("Permission:Feedbacks"));
        feedbackPermission.AddChild(abpPermissions.Feedbacks.Create, L("Permission:Create"));
        feedbackPermission.AddChild(abpPermissions.Feedbacks.Edit, L("Permission:Edit"));
        feedbackPermission.AddChild(abpPermissions.Feedbacks.Delete, L("Permission:Delete"));

        var partPermission = myGroup.AddPermission(abpPermissions.Parts.Default, L("Permission:Parts"));
        partPermission.AddChild(abpPermissions.Parts.Create, L("Permission:Create"));
        partPermission.AddChild(abpPermissions.Parts.Edit, L("Permission:Edit"));
        partPermission.AddChild(abpPermissions.Parts.Delete, L("Permission:Delete"));

        var testPermission = myGroup.AddPermission(abpPermissions.Tests.Default, L("Permission:Tests"));
        testPermission.AddChild(abpPermissions.Tests.Create, L("Permission:Create"));
        testPermission.AddChild(abpPermissions.Tests.Edit, L("Permission:Edit"));
        testPermission.AddChild(abpPermissions.Tests.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<abpResource>(name);
    }
}