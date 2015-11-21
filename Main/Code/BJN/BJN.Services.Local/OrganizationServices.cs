using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJN.Data;
using BJN.Data.EntityFramework;
using BJN.Domain.Entities;

namespace BJN.Services.Local
{
    public class OrganizationServices : IOrganizationServices
    {
        private readonly IUnitOfWork _database;

        public OrganizationServices()
        {
            _database = new UnitOfWork();
        }

        protected IUnitOfWork Database
        {
            get { return _database; }
        }

        public IQueryable<Organization> GetOrganizations()
        {
            return Database.Organizations.GetAll();
        }

        public Organization GetOrganizationById(int id)
        {
            return Database.Organizations.GetById(id);
        }

        public void CreateOrganization(Organization organization)
        {
            Database.Organizations.Add(organization);
            Database.Commit();
        }

        public void UpdateOrganization(Organization organization)
        {
            Database.Organizations.Update(organization);
            Database.Commit();
        }

        public void DeleteOrganization(Organization organization)
        {
            Database.Organizations.Delete(organization);
            Database.Commit();
        }

        public void DeleteOrganization(int id)
        {
            Database.Organizations.Delete(id);
            Database.Commit();
        }
    }
}
