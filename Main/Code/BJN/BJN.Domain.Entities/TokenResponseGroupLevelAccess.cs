using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Domain.Entities
{
    public class TokenResponseGroupLevelAccess
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public ScopeGroup scope { get; set; }
        public object refresh_token { get; set; }
    }

    public class ScopeGroup
    {
        public int enterprise { get; set; }
        public string partitionName { get; set; }
        public PartitionGroup partition { get; set; }
    }
    public class PartitionGroup
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
