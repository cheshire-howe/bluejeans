using System.Data.Entity;
using BJN.Domain.Entities;
using BJN.Services.Connectors.Lti.Lti;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BJN.Services.Connectors.Lti.Models
{
    public class ProviderContext : IdentityDbContext<LtiUser>
    {
        public ProviderContext() : base("BjnContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new ToolProviderDbInitializer<ProviderContext>());
        }

        public static ProviderContext Create()
        {
            return new ProviderContext();
        }

        public DbSet<Tool> Tools { get; set; }

        // From LtiLibrary
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<ProviderRequest> ProviderRequests { get; set; }
        public DbSet<Outcome> Outcomes { get; set; }

        // Lti Tool Stuff
        public DbSet<LtiMeeting> LtiMeetings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Identity stuff
            modelBuilder.HasDefaultSchema("LtiProvider");
            modelBuilder.Entity<LtiUser>().ToTable("LtiUsers");
            modelBuilder.Entity<IdentityRole>().ToTable("LtiRoles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("LtiUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("LtiUserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("LtiUserClaims");
        }
    }

    public class ToolProviderDbInitializer<T> : DropCreateDatabaseIfModelChanges<ProviderContext>
    {
        protected override void Seed(ProviderContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(UserRoles.AdminRole)) roleManager.Create(new IdentityRole(UserRoles.AdminRole));
            if (!roleManager.RoleExists(UserRoles.SuperUserRole)) roleManager.Create(new IdentityRole(UserRoles.SuperUserRole));
            if (!roleManager.RoleExists(UserRoles.TeacherRole)) roleManager.Create(new IdentityRole(UserRoles.TeacherRole));
            if (!roleManager.RoleExists(UserRoles.StudentRole)) roleManager.Create(new IdentityRole(UserRoles.StudentRole));

            base.Seed(context);
        }
    }
}