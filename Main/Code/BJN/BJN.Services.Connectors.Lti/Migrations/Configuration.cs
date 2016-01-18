using BJN.Services.Connectors.Lti.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BJN.Services.Connectors.Lti.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BJN.Services.Connectors.Lti.Models.ProviderContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BJN.Services.Connectors.Lti.Models.ProviderContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(UserRoles.AdminRole)) roleManager.Create(new IdentityRole(UserRoles.AdminRole));
            if (!roleManager.RoleExists(UserRoles.SuperUserRole)) roleManager.Create(new IdentityRole(UserRoles.SuperUserRole));
            if (!roleManager.RoleExists(UserRoles.TeacherRole)) roleManager.Create(new IdentityRole(UserRoles.TeacherRole));
            if (!roleManager.RoleExists(UserRoles.StudentRole)) roleManager.Create(new IdentityRole(UserRoles.StudentRole));
        }
    }
}
