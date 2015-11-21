using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJN.Domain.Entities;
using BJN.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BJN.Data.EntityFramework
{
    public class BjnContext : IdentityDbContext<ApplicationUser>
    {
        public BjnContext() : base("name=BjnContext")
        {
            Database.SetInitializer<BjnContext>(new BjnInitializer<BjnContext>());
        }

        public static BjnContext Create()
        {
            return new BjnContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Identity stuff
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
        }
    }

    public class BjnInitializer<T> : DropCreateDatabaseIfModelChanges<BjnContext> where T : DbContext
    {
        protected override void Seed(BjnContext context)
        {
            var organization = new Organization()
            {
                Name = "ThreePointTurn",
                AppKey = "1234567890",
                AppSecret = "3772e1380d344d4a8da3154e6a0d5753"
            };
            context.Set<Organization>().AddOrUpdate(organization);

            string[] roles = {"Admin", "User"};
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            foreach (var role in roles)
            {
                if (roleManager.RoleExists(role) == false)
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var passwordHash = new PasswordHasher();
            var user = new ApplicationUser
            {
                UserName = "josh.zoff@threepointturn.com",
                Email = "josh.zoff@threepointturn.com",
                PasswordHash = passwordHash.HashPassword("`1q2w3e4r5t"),
                BjnID = 594544,
                EnterpriseUser = true,
                Organization = organization
            };

            userManager.Create(user);
            userManager.AddToRole(user.Id, roles[0]);

            base.Seed(context);
        }
    }
}
