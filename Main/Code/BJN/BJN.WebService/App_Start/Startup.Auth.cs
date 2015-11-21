using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BJN.Services;
using BJN.Services.Local;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace BJN.WebService
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            SecurityServices.ConfigureAuth(app);
        }
    }
}