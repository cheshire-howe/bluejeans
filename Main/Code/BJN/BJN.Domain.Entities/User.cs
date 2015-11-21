using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Domain.Entities
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailId { get; set; }
        public string company { get; set; }
        public string middleName { get; set; }
        public string title { get; set; }
        public string phone { get; set; }
        public string profilePicture { get; set; }
        public string timezone { get; set; }
        public int timeFormat { get; set; }
        public string language { get; set; }
        public string skypeId { get; set; }
        public string gtalkId { get; set; }
        public string defaultEndpoint { get; set; }
        public bool passwordChangeRequired { get; set; }
        public int marketoId { get; set; }
        public bool optOutOffers { get; set; }
        public bool optOutNews { get; set; }
        public string geoInfo { get; set; }
        public string howDidYouHear { get; set; }
        public object sfdcToken { get; set; }
        public object linkedinProfileUrl { get; set; }
        public long lastLogin { get; set; }
        public long dateJoined { get; set; }
        public object jid { get; set; }
        public int channel_id { get; set; }
    }
}
