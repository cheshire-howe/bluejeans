using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJN.Domain.Entities;

namespace BJN.Services
{
    public interface IOrganizationServices
    {
        IQueryable<Organization> GetOrganizations();
        Organization GetOrganizationById(int id);
        void CreateOrganization(Organization organization);
        void UpdateOrganization(Organization organization);
        void DeleteOrganization(Organization organization);
        void DeleteOrganization(int id);
    }
}
