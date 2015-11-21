using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJN.Domain.Entities;
using BJN.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using User = BJN.Domain.Entities.Identity.User;

namespace BJN.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Contexts
        private BjnContext _bjnContext;

        
        internal BjnContext BjnContext
        {
            get
            {
                if (_bjnContext == null)
                    _bjnContext = new BjnContext();
                return _bjnContext;
            }
        }

        #endregion

        #region Repositories

        private IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

        private IRepository<T> GetRepository<T>(DbContext context) where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
                _repositories.Add(typeof(T), new GenericRepository<T>(context));
            return (IRepository<T>)_repositories[typeof(T)];
        }

        /*public IRepository<ExternalLogin> ExternalLoginRepository { get { return GetRepository<ExternalLogin>(BjnContext); } }
        public IRepository<Role> RoleRepository { get { return GetRepository<Role>(BjnContext); } }
        public IRepository<User> UserRepository { get { return GetRepository<User>(BjnContext); } }*/

        /*public IUserStore<ApplicationUser> UserStore { get { return new UserStore<ApplicationUser>(BjnContext); } }*/

        public IRepository<ApplicationUser> ApplicationUsers { get { return GetRepository<ApplicationUser>(BjnContext); } }
        public IRepository<Organization> Organizations { get { return GetRepository<Organization>(BjnContext); } }

        #endregion

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            if (_bjnContext != null)
                _bjnContext.SaveChanges();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_bjnContext != null)
                    _bjnContext.Dispose();
            }
        }

        #endregion 
    }
}
