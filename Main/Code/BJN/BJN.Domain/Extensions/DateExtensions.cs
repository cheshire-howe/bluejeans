using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Domain.Extensions
{
    public static class DateExtensions
    {
        public static DateTime UnixTimestampToDateTime(this long unixTimestamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimestamp / 1000).ToLocalTime();
            return dtDateTime;
        }
    }
}
