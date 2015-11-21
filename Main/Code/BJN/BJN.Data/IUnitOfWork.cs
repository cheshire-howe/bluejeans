using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJN.Domain.Entities;
using BJN.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using User = BJN.Domain.Entities.Identity.User;

namespace BJN.Data
{
    public interface IUnitOfWork
    {
        /*IRepository<ExternalLogin> ExternalLoginRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<User> UserRepository { get; }*/

        IRepository<ApplicationUser> ApplicationUsers { get; }
        IRepository<Organization> Organizations { get; }

        void Commit();

        //IUserStore<ApplicationUser> UserStore { get; }
    }
}
