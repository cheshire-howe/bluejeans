using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BJN.Domain;
using BJN.Domain.Entities;
using BJN.Services.Local;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BJN.Test.UnitTesting
{
    [TestClass]
    public class BJNDomainTests
    {
        private readonly BJNApiClient _apiClient;

        public ApplicationUser User
        {
            get
            {
                return new ApplicationUser()
                {
                    BjnID = 594544,
                    Email = "josh.zoff@threepointturn.com",
                    Organization = new Organization()
                    {
                        AppKey = "1234567890",
                        AppSecret = "3772e1380d344d4a8da3154e6a0d5753"
                    }
                };
            }
        }

        public BJNDomainTests()
        {
            _apiClient = new BJNApiClient();
            _apiClient.GetOauthToken(User).Wait();
        }

        [TestMethod]
        public void GetOauthToken()
        {
            Assert.IsNotNull(_apiClient.Token);
        }

        /*[TestMethod]
        public async Task GetUserToken()
        {
            var client = new BJNApiClient();
            await client.GetUserToken("josh.zoffranieri", "3pointturn", User);

            Assert.IsNotNull(client.Token);
        }*/

        [TestMethod]
        public async Task GetMeetings()
        {
            // Get all meetings
            HttpResponseMessage response = await _apiClient.GetScheduledMeetings();

            // Deserialize json to view the body of the response for debug purposes
            dynamic content = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            var json = ((object)content).ToString();

            Debug.WriteLine(response);
            Debug.WriteLine(json);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task ScheduleMeeting()
        {
            var meeting = JsonConvert.DeserializeObject<Meeting>(_mockJsonData);

            HttpResponseMessage response = await _apiClient.ScheduleMeeting(meeting);

            Debug.WriteLine(response);

            Assert.IsTrue(meeting.advancedMeetingOptions.autoRecord);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void EndMeeting()
        {
            // Should trigger send email event with link to download recording
        }

        private string _mockJsonData = @"
        {
            ""title"": ""Test"",
            ""description"": ""My meeting"",
            ""start"": 1532310400000,
            ""end"": 1532314000000,
            ""timezone"": ""America/New_York"",
            ""advancedMeetingOptions"": {
                ""videoBestFit"": false,
                ""publishMeeting"": false,
                ""encryptionType"": ""NO_ENCRYPTION"",
                ""moderatorLess"": false,
                ""allowStream"": false,
                ""autoRecord"": true,
                ""disallowChat"": false,
                ""muteParticipantsOnEntry"": false,
                ""showAllAttendeesInMeetingInvite"": false
            },
            ""addAttendeePasscode"": false,
            ""deleted"": false,
            ""allow720p"": false,
            ""status"": null,
            ""locked"": false,
            ""sequenceNumber"": 0,
            ""endPointType"": ""WEB_APP"",
            ""endPointVersion"": ""1.80"",
            ""attendees"": [
                {
                    ""email"": ""josh.zoff@example.com""
                }
            ]
        }";
    }
}
