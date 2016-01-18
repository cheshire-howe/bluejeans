using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BJN.Domain;
using BJN.Domain.Entities;
using BJN.Services.Connectors.Web;
using BJN.Services.Local;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace BJN.WebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MeetingController : ApiController
    {
        private readonly BJNApiClient _apiClient;
        private readonly ApplicationUserManager _userManager;

        public MeetingController()
        {
            _apiClient = new BJNApiClient();
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        // GET: api/Meeting
        //[Route("api/Meeting/{email}")]
        public async Task<HttpResponseMessage> Get(string email)
        {
            email = HttpUtility.UrlDecode(email);
            var user = _userManager.GetUsers().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new JsonContent(new
                    {
                        Message = "User does not exist"
                    })
                };
            }

            await _apiClient.GetToken(user);

            return await _apiClient.GetScheduledMeetings();
        }

        // GET: api/Meeting/5
        public async Task<HttpResponseMessage> Get(int id, string email)
        {
            email = HttpUtility.UrlDecode(email);
            var user = _userManager.GetUsers().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new JsonContent(new
                    {
                        Message = "User does not exist"
                    })
                };
            }

            await _apiClient.GetToken(user);

            return await _apiClient.GetScheduledMeeting(id);
        }

        [Route("api/Meeting/{meetingid}/SendDownloadEmail")]
        public async Task<HttpResponseMessage> GetDownloadEmail(int meetingid, string email)
        {
            email = HttpUtility.UrlDecode(email);
            var user = _userManager.GetUsers().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new JsonContent(new
                    {
                        Message = "User does not exist"
                    })
                };
            }

            await _apiClient.GetToken(user);

            return await _apiClient.SendDownloadEmail(meetingid);
        }

        [Route("api/Meeting/{numericmeetingid}/GetDownloadUrls")]
        public async Task<HttpResponseMessage> GetDownloadUrls(string numericmeetingid, string email)
        {
            email = HttpUtility.UrlDecode(email);
            var user = _userManager.GetUsers().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new JsonContent(new
                    {
                        Message = "User does not exist"
                    })
                };
            }

            await _apiClient.GetToken(user);

            return await _apiClient.GetDownloadUrls(numericmeetingid);
        }

        [Route("api/Meeting/GetCurrentMeetings")]
        public async Task<HttpResponseMessage> GetCurrentMeetings(string email)
        {
            email = HttpUtility.UrlDecode(email);
            var user = _userManager.GetUsers().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new JsonContent(new
                    {
                        Message = "User does not exist"
                    })
                };
            }

            await _apiClient.GetToken(user);

            return await _apiClient.GetCurrentMeetings();
        }

        // POST: api/Meeting
        public async Task<HttpResponseMessage> Post(Meeting meeting, string email)
        {
            email = HttpUtility.UrlDecode(email);
            var user = _userManager.GetUsers().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new JsonContent(new
                    {
                        Message = "User does not exist"
                    })
                };
            }

            if (meeting.recurrencePattern == null)
            {
                meeting.recurrencePattern = null; //new RecurrencePattern() { };
            }

            await _apiClient.GetToken(user);

            return await _apiClient.ScheduleMeeting(meeting);
        }

        // PUT: api/Meeting/5
        public async Task<HttpResponseMessage> Put(int id, Meeting meeting, string email)
        {
            email = HttpUtility.UrlDecode(email);
            var user = _userManager.GetUsers().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new JsonContent(new
                    {
                        Message = "User does not exist"
                    })
                };
            }

            await _apiClient.GetToken(user);

            return await _apiClient.UpdateMeeting(id, meeting);
        }

        // DELETE: api/Meeting/5
        public async Task<HttpResponseMessage> Delete(int id, string email)
        {
            email = HttpUtility.UrlDecode(email);
            var user = _userManager.GetUsers().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new JsonContent(new
                    {
                        Message = "User does not exist"
                    })
                };
            }

            await _apiClient.GetToken(user);

            return await _apiClient.CancelMeeting(id);
        }
    }
}
