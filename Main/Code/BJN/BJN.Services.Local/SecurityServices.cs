using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJN.Data;
using BJN.Data.EntityFramework;
using BJN.Domain.Entities;
using BJN.Domain.Entities.Identity;
using BJN.Services.Connectors.Lti;
using BJN.Services.Connectors.OAuth2;
using BJN.Services.Connectors.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;

namespace BJN.Services.Local
{
    public class SecurityServices : UserStore<ApplicationUser>, ISecurityServices
    {
        public static void ConfigureAuth(IAppBuilder app)
        {
            new WebConnector().ConfigureAuth(app);
            new LtiConnector().ConfigureAuth(app);
        }
    }
}
