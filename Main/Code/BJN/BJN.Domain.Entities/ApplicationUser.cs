using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BJN.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            //userIdentity.AddClaim(new Claim("FullName", FullName));
            return userIdentity;
        }

        public int BjnID { get; set; }
        public int OrganizationID { get; set; }
        public bool EnterpriseUser { get; set; }
        public string BjnUsername { get; set; }
        public string BjnPassword { get; set; }
        public Organization Organization { get; set; }
    }
}
