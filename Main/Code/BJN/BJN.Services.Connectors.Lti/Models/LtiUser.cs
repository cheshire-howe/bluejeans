using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BJN.Services.Connectors.Lti.Models
{
    public class LtiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string FullName
        {
            get
            {
                var fullname = string.Format("{0} {1}", FirstName, LastName).Trim();
                return string.IsNullOrEmpty(fullname) ? "n/a" : fullname;
            }
        }
        public string LastName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<LtiUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
