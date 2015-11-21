using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Domain.Entities
{
    public class TokenResponseUserPermissions
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public Scope scope { get; set; }
        public string refresh_token { get; set; }
    }

    public class Scope
    {
        public int user { get; set; }
        public string partitionName { get; set; }
        public Partition partition { get; set; }
    }

    public class Partition
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
