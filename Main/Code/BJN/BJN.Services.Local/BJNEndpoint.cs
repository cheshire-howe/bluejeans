using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Services.Local
{
    public class BJNEndpoint
    {
        // TODO: Get from custom config
        private const string VersionPrefix = "v1/";

        public static string BaseUrl
        {
            // TODO: Get from custom config
            get { return "https://api.bluejeans.com"; }
        }

        public static string AppKey
        {
            // TODO: Get from custom config - this will move to plugin
            get { return "1234567891"; }
        }

        public static string AppSecret
        {
            // TODO: Get from custom config - this will move to plugin
            get { return "4acfc4c8924d40389f4b63c7322dc7a0"; }
        }

        public static string Token
        {
            get { return "oauth2/token"; }
        }

        public static string User(int userid, string token)
        {
            return string.Format("{0}user/{1}/?access_token={2}", VersionPrefix, userid, token);
        }

        public static string Meetings(int userid, string token)
        {
            return string.Format("{0}user/{1}/scheduled_meeting/?access_token={2}", VersionPrefix, userid, token);
        }

        public static string Meetings(int userid, string token, bool withEmail)
        {
            var endpoint = new StringBuilder();
            endpoint.Append(string.Format("{0}user/{1}/scheduled_meeting/?access_token={2}", VersionPrefix, userid, token));

            if (withEmail)
            {
                endpoint.Append("&email=true");
            }

            return endpoint.ToString();
        }

        public static string MeetingById(int userid, string token, int meetingid, bool withEmail)
        {
            var endpoint = new StringBuilder();
            endpoint.Append(string.Format("{0}user/{1}/scheduled_meeting/{3}/?access_token={2}", VersionPrefix, userid, token, meetingid));

            if (withEmail)
            {
                endpoint.Append("&email=true");
            }
            return endpoint.ToString();
        }

        public static string MeetingHistoryById(int userid, string token, string numericMeetingid)
        {
            return string.Format("{0}user/{1}/meeting_history/{3}/recordings?access_token={2}",
                VersionPrefix, userid, token, numericMeetingid);
        }

        public static string MeetingContent(int userid, string token, string contentid)
        {
            return string.Format("{0}user/{1}/cms/{3}?access_token={2}&isDownloadable=true", VersionPrefix, userid, token, contentid);
        }

        public static string UserRoom(int userid, string token)
        {
            return string.Format("{0}user/{1}/room/?access_token={2}", VersionPrefix, userid, token);
        }
    }
}
