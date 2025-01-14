using abp.TestTemplates;
using abp.Templates;
using abp.Tests;
using abp.Parts;
using abp.Feedbacks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;

namespace abp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class abpDbContext :
    AbpDbContext<abpDbContext>,
    ISaasDbContext,
    IIdentityProDbContext
{
    public DbSet<TestTemplate> TestTemplates { get; set; } = null!;
    public DbSet<Template> Templates { get; set; } = null!;
    public DbSet<Test> Tests { get; set; } = null!;
    public DbSet<Part> Parts { get; set; } = null!;
    public DbSet<Feedback> Feedbacks { get; set; } = null!;
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public abpDbContext(DbContextOptions<abpDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureGdpr();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(abpConsts.DbTablePrefix + "YourEntities", abpConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Part>(b =>
            {
                b.ToTable(abpConsts.DbTablePrefix + "Parts", abpConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.name).HasColumnName(nameof(Part.name));
                b.Property(x => x.number).HasColumnName(nameof(Part.number));
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Test>(b =>
            {
                b.ToTable(abpConsts.DbTablePrefix + "Tests", abpConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.name).HasColumnName(nameof(Test.name));
                b.Property(x => x.Age).HasColumnName(nameof(Test.Age));
                b.Property(x => x.DOB).HasColumnName(nameof(Test.DOB));
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Template>(b =>
            {
                b.ToTable(abpConsts.DbTablePrefix + "Templates", abpConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.name).HasColumnName(nameof(Template.name));
                b.Property(x => x.number).HasColumnName(nameof(Template.number));
                b.Property(x => x.description).HasColumnName(nameof(Template.description));
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<TestTemplate>(b =>
            {
                b.ToTable(abpConsts.DbTablePrefix + "TestTemplates", abpConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.name).HasColumnName(nameof(TestTemplate.name));
                b.Property(x => x.number).HasColumnName(nameof(TestTemplate.number));
                b.Property(x => x.description).HasColumnName(nameof(TestTemplate.description));
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Feedback>(b =>
            {
                b.ToTable(abpConsts.DbTablePrefix + "Feedbacks", abpConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.name).HasColumnName(nameof(Feedback.name));
                b.Property(x => x.number).HasColumnName(nameof(Feedback.number)).HasMaxLength(FeedbackConsts.numberMaxLength);
            });

        }
    }
}