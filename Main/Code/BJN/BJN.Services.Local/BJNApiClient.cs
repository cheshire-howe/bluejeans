using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BJN.Domain.Entities;
using Newtonsoft.Json;

namespace BJN.Services.Local
{
    public class BJNApiClient
    {
        private readonly string _baseUrl = BJNEndpoint.BaseUrl;
        private readonly IConnector _connector;

        public BJNApiClient()
        {
            _connector = new JsonConnector();
        }

        // TODO: Cache the token, potentially in Memcached or Redis
        public string Token { get; set; }

        public int UserId { get; set; }

        public async Task GetToken(ApplicationUser user)
        {
            if (user.EnterpriseUser)
            {
                await GetOauthToken(user);
            }
            else
            {
                await GetUserToken(user);
            }
        }

        public async Task GetOauthToken(ApplicationUser user)
        {
            var endpoint = BJNEndpoint.Token;
            var payload = JsonConvert.SerializeObject(new TokenRequestGroupLevelAccess()
            {
                grant_type = "client_credentials",
                client_id = user.Organization.AppKey,
                client_secret = user.Organization.AppSecret
            });

            try
            {
                var response = await _connector.PostAsync(_baseUrl, endpoint, payload);

                var content = JsonConvert.DeserializeObject<TokenResponseGroupLevelAccess>(await response.Content.ReadAsStringAsync());

                Token = content.access_token;
                UserId = user.BjnID;
            }
            catch (HttpRequestException)
            {
                // TODO: Handle error gracefully
                throw;
            }
        }

        public async Task GetUserToken(ApplicationUser user)
        {
            var endpoint = BJNEndpoint.Token;
            var payload = JsonConvert.SerializeObject(new TokenRequestUserPermissions()
            {
                grant_type = "password",
                username = user.BjnUsername,
                password = user.BjnPassword
            });

            try
            {
                var response = await _connector.PostAsync(_baseUrl, endpoint, payload);

                var content = JsonConvert.DeserializeObject<TokenResponseGroupLevelAccess>(await response.Content.ReadAsStringAsync());

                Token = content.access_token;
                UserId = user.BjnID; //content.scope.user;
            }
            catch (HttpRequestException)
            {
                // TODO: Handle error gracefully
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetScheduledMeetings()
        {
            var endpoint = BJNEndpoint.Meetings(UserId, Token);

            try
            {
                return await _connector.GetAsync(_baseUrl, endpoint);
            }
            catch (HttpRequestException)
            {
                // TODO: Handle error gracefully
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetScheduledMeeting(int id)
        {
            var endpoint = BJNEndpoint.MeetingById(UserId, Token, id, true);

            try
            {
                return await _connector.GetAsync(_baseUrl, endpoint);
            }
            catch (Exception)
            {
                // TODO: Handle error gracefully
                throw;
            }
        }

        public async Task<HttpResponseMessage> ScheduleMeeting(Meeting newMeeting)
        {
            var endpoint = BJNEndpoint.Meetings(UserId, Token, true);

            //newMeeting.advancedMeetingOptions.autoRecord = true;

            var payload = JsonConvert.SerializeObject(newMeeting);

            try
            {
                return await _connector.PostAsync(_baseUrl, endpoint, payload);
            }
            catch (HttpRequestException)
            {
                // TODO: Handle error gracefully
                throw;
            }
        }

        public async Task<HttpResponseMessage> UpdateMeeting(int id, Meeting meeting)
        {
            var endpoint = BJNEndpoint.MeetingById(UserId, Token, id, true);

            var payload = JsonConvert.SerializeObject(meeting);

            try
            {
                return await _connector.PutAsync(_baseUrl, endpoint, payload);
            }
            catch (Exception)
            {
                // TODO: Handle error gracefully
                throw;
            }
        }

        public async Task<HttpResponseMessage> CancelMeeting(int meetingId)
        {
            var endpoint = BJNEndpoint.MeetingById(UserId, Token, meetingId, false);

            try
            {
                return await _connector.DeleteAsync(_baseUrl, endpoint);
            }
            catch (Exception)
            {
                // TODO: Handle error gracefully
                throw;
            }
        }

        public async Task<HttpResponseMessage> SendDownloadEmail(int meetingid)
        {
            var meetingjson = await GetScheduledMeeting(meetingid);

            var meeting = JsonConvert.DeserializeObject<Meeting>(
                await meetingjson.Content.ReadAsStringAsync());

            var downloadUrls = await DownloadUrls(meeting.numericMeetingId);

            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(downloadUrls), Encoding.UTF8, "application/json")
            };

            var emailer = new Emailer();

            foreach (var attendee in meeting.attendees)
            {
                emailer.SendEmail(attendee.email, meeting, downloadUrls);
            }

            return response;
        }

        public async Task<HttpResponseMessage> GetDownloadUrls(string numericMeetingId)
        {
            return new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(await DownloadUrls(numericMeetingId)),
                    Encoding.UTF8, "application/json")
            };
        }

        public async Task<HttpResponseMessage> GetCurrentMeetings()
        {
            var meetingResponse = await GetScheduledMeetings();

            var meetings = JsonConvert.DeserializeObject<List<Meeting>>(await meetingResponse.Content.ReadAsStringAsync());

            var currentMeeting = new List<KeyValuePair<int, string>>();
            foreach (var meeting in meetings)
            {
                if (UnixTimeToDateTime(meeting.start) < DateTime.UtcNow && UnixTimeToDateTime(meeting.end) > DateTime.UtcNow)
                {
                    var m = new KeyValuePair<int, string>(
                        meeting.id, "https://bluejeans.com/" + meeting.numericMeetingId);
                    currentMeeting.Add(m);
                }
            }

            return new HttpResponseMessage
            {
                Content =
                    new StringContent(JsonConvert.SerializeObject(currentMeeting), Encoding.UTF8, "application/json")
            };
        }

        private async Task<List<string>> DownloadUrls(string numericMeetingId)
        {
            var endpointMeetingHistory = BJNEndpoint.MeetingHistoryById(UserId, Token, numericMeetingId);

            var meetingHistoryResponse = await _connector.GetAsync(_baseUrl, endpointMeetingHistory);

            dynamic meetingHistory = JsonConvert.DeserializeObject(await meetingHistoryResponse.Content.ReadAsStringAsync());

            var downloadUrls = new List<String>();
            if (meetingHistory.Count > 0)
            {
                foreach (var mHist in meetingHistory)
                {
                    foreach (var recording in mHist.recordingSessions)
                    {
                        var contentId = recording.contentId.ToString();
                        var contentjson = await _connector.GetAsync(_baseUrl, BJNEndpoint.MeetingContent(UserId, Token, contentId));
                        var content = JsonConvert.DeserializeObject(await contentjson.Content.ReadAsStringAsync());
                        downloadUrls.Add(content.contentUrl.ToString());
                    }
                }
            }

            return downloadUrls;
        }

        public async Task<HttpResponseMessage> GetUserRoomSettings()
        {
            var endpoint = BJNEndpoint.UserRoom(UserId, Token);

            return await _connector.GetAsync(_baseUrl, endpoint);
        }

        // TODO: Add extension method to datetime
        private long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalSeconds;
        }

        // TODO: Add extension method to long
        public static DateTime UnixTimeToDateTime(long unixtime)
        {
            unixtime = unixtime / 1000;
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddSeconds(unixtime);
        }
    }
}
