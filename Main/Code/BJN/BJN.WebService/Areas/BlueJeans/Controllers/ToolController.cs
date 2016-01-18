using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BJN.Domain.Entities;
using BJN.Services.Connectors.Lti.Identity;
using BJN.Services.Connectors.Lti.Models;
using BJN.Services.Connectors.Web;
using BJN.Services.Local;
using BJN.WebService.Areas.BlueJeans.Helpers;
using BJN.WebService.Areas.BlueJeans.Models;
using BJN.WebService.Areas.LtiProvider.Filters;
using LtiLibrary.Core.Lti1;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace BJN.WebService.Areas.BlueJeans.Controllers
{
    [XFrameOptionsHeader]
    public class ToolController : Controller
    {
        private readonly ProviderContext _providerContext;
        private readonly LtiUserManager _ltiUserManager;
        private readonly ApplicationUserManager _applicationUserManager;
        private BJNApiClient _apiClient;

        public ToolController(ProviderContext providerContext,
            LtiUserManager ltiUserManager,
            ApplicationUserManager applicationUserManager,
            BJNApiClient apiClient)
        {
            _providerContext = providerContext;
            _ltiUserManager = ltiUserManager;
            _applicationUserManager = applicationUserManager;
            _apiClient = apiClient;
        }

        [Authorize]
        [ChildActionOnly]
        public ActionResult Context()
        {
            var ltiRequest = GetLtiRequestFromClaim();

            var consumer = _providerContext.Consumers.SingleOrDefault(c => c.Key.Equals(ltiRequest.ConsumerKey));
            if (consumer == null) return null;

            var toolUser = new ToolUser
            {
                ConsumerName = consumer.Name,
                FirstName = ltiRequest.LisPersonNameGiven,
                LastName = ltiRequest.LisPersonNameFamily,
                ReturnUrl = ltiRequest.LaunchPresentationReturnUrl,
                Roles = ltiRequest.Roles
            };

            var roles = toolUser.Roles.Split(',');

            if (roles.Contains("Instructor"))
            {
                _ltiUserManager.AddToRole(User.Identity.GetUserId(), UserRoles.TeacherRole);
            }
            else if (roles.Contains("Learner"))
            {
                _ltiUserManager.AddToRole(User.Identity.GetUserId(), UserRoles.StudentRole);
            }

            return PartialView("_ContextPartial", toolUser);
        }

        // GET: BlueJeans/Tool
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var ltiRequest = GetLtiRequestFromClaim();
            if (ltiRequest == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid LTI request" });

            var consumer = _providerContext.Consumers.SingleOrDefault(c => c.Key.Equals(ltiRequest.ConsumerKey));
            if (consumer == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid Consumer" });

            var meetings = _providerContext.LtiMeetings.Where(x => x.CourseId == ltiRequest.ContextId);

            var ltiMeetingViewModels = new List<LtiMeetingViewModel>();
            foreach (var ltiMeeting in meetings)
            {
                var meetingUser = _applicationUserManager.GetUsers().FirstOrDefault(x => x.Email == ltiMeeting.TeacherEmail);
                await _apiClient.GetToken(meetingUser);

                var response = await _apiClient.GetScheduledMeeting(ltiMeeting.MeetingId);
                var ltiMeetingViewModel =
                    JsonConvert.DeserializeObject<LtiMeetingViewModel>(await response.Content.ReadAsStringAsync());
                ltiMeetingViewModel.LtiMeetingId = ltiMeeting.ID;
                ltiMeetingViewModels.Add(ltiMeetingViewModel);
            }

            ViewBag.CourseId = ltiRequest.ContextId;

            return View(ltiMeetingViewModels);
        }

        // GET: BlueJeans/Tool/Create
        public ActionResult Create()
        {
            var ltiRequest = GetLtiRequestFromClaim();
            if (ltiRequest == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid LTI request" });

            var consumer = _providerContext.Consumers.SingleOrDefault(c => c.Key.Equals(ltiRequest.ConsumerKey));
            if (consumer == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid Consumer" });

            var ltiMeetingViewModel = new LtiMeetingViewModel();

            return View(ltiMeetingViewModel);
        }

        // POST: BlueJeans/Tool/Create
        [HttpPost]
        public async Task<ActionResult> Create(LtiMeetingViewModel ltiMeetingViewModel)
        {
            var ltiRequest = GetLtiRequestFromClaim();
            if (ltiRequest == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid LTI request" });

            var consumer = _providerContext.Consumers.SingleOrDefault(c => c.Key.Equals(ltiRequest.ConsumerKey));
            if (consumer == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid Consumer" });

            var meeting = Mapper.MapLtiMeetingViewModelToMeeting(ltiMeetingViewModel);

            var email = User.Identity.GetUserName();
            var appUser = _applicationUserManager.GetUsers().FirstOrDefault(x => x.Email == email);

            if (appUser == null)
                return RedirectToAction("BadRequest", "Error",
                    new {error = "You do not have the correct permissions to access this feature"});

            await _apiClient.GetToken(appUser);

            var response = await _apiClient.ScheduleMeeting(meeting);
            var meetingResponse = JsonConvert.DeserializeObject<Meeting>(await response.Content.ReadAsStringAsync());

            var ltiMeeting = new LtiMeeting
            {
                Consumer = consumer,
                CourseId = ltiRequest.ContextId,
                Title = ltiMeetingViewModel.title,
                MeetingId = meetingResponse.id,
                TeacherEmail = appUser.Email
            };

            _providerContext.LtiMeetings.Add(ltiMeeting);
            _providerContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: BlueJeans/Tool/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var ltiRequest = GetLtiRequestFromClaim();
            if (ltiRequest == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid LTI request" });

            var consumer = _providerContext.Consumers.SingleOrDefault(c => c.Key.Equals(ltiRequest.ConsumerKey));
            if (consumer == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid Consumer" });

            var ltiMeeting = _providerContext.LtiMeetings.FirstOrDefault(x => x.ID == id);
            if (ltiMeeting == null)
            {
                return RedirectToAction("BadRequest", "Error", new { error = "Meeting not found" });
            }

            var appUser = _applicationUserManager.GetUsers().FirstOrDefault(x => x.Email == ltiMeeting.TeacherEmail);
            await _apiClient.GetToken(appUser);
            var meetingResponse = await _apiClient.GetScheduledMeeting(ltiMeeting.MeetingId);

            var bjnMeeting = JsonConvert.DeserializeObject<LtiMeetingViewModel>(await meetingResponse.Content.ReadAsStringAsync());
            
            if (bjnMeeting.recurrencePattern != null)
            {
                bjnMeeting.recurrencePatternValues = Utils.GetRecurrencePatternValues(bjnMeeting.recurrencePattern);
            }

            bjnMeeting.durationString = Utils.GetClassDurationString(bjnMeeting.start, bjnMeeting.end);

            var userRoomSettingsResponse = await _apiClient.GetUserRoomSettings();
            var userRoomSettings =
                JsonConvert.DeserializeObject<UserRoom>(await userRoomSettingsResponse.Content.ReadAsStringAsync());

            ViewBag.LtiMeetingId = ltiMeeting.ID;
            ViewBag.ModeratorPasscode = userRoomSettings.moderatorPasscode;

            return View(bjnMeeting);
        }

        // GET: BlueJeans/Tool/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var ltiRequest = GetLtiRequestFromClaim();
            if (ltiRequest == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid LTI request" });

            var consumer = _providerContext.Consumers.SingleOrDefault(c => c.Key.Equals(ltiRequest.ConsumerKey));
            if (consumer == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid Consumer" });

            var email = User.Identity.GetUserName();
            var appUser = _applicationUserManager.GetUsers().FirstOrDefault(x => x.Email == email);

            if (appUser == null)
            {
                return RedirectToAction("BadRequest", "Error", new { error = "Not enough permissions to access this page" });
            }

            var ltiMeeting = _providerContext.LtiMeetings.Find(id);
            if (ltiMeeting == null)
            {
                return RedirectToAction("BadRequest", "Error", new { error = "Meeting not found" });
            }

            await _apiClient.GetToken(appUser);
            var meetingResponse = await _apiClient.GetScheduledMeeting(ltiMeeting.MeetingId);

            var bjnMeeting = JsonConvert.DeserializeObject<LtiMeetingViewModel>(await meetingResponse.Content.ReadAsStringAsync());

            var temp = (bjnMeeting.end - bjnMeeting.start)/1000/60; // milliseconds to minutes
            bjnMeeting.lengthOfClass = (int)temp;
            bjnMeeting.attendeesString = String.Join(", ", bjnMeeting.attendees.Select(x => x.email).ToArray());

            return View(bjnMeeting);
        }

        // POST: BlueJeans/Tool/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, LtiMeetingViewModel ltiMeetingViewModel)
        {
            var ltiRequest = GetLtiRequestFromClaim();
            if (ltiRequest == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid LTI request" });

            var consumer = _providerContext.Consumers.SingleOrDefault(c => c.Key.Equals(ltiRequest.ConsumerKey));
            if (consumer == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid Consumer" });

            var meeting = Mapper.MapLtiMeetingViewModelToMeeting(ltiMeetingViewModel);
            var ltiMeeting = _providerContext.LtiMeetings.Find(id);

            var email = User.Identity.GetUserName();
            var appUser = _applicationUserManager.GetUsers().FirstOrDefault(x => x.Email == email);

            await _apiClient.GetToken(appUser);
            var response = await _apiClient.UpdateMeeting(ltiMeeting.MeetingId, meeting);
            var bjnMeeting = JsonConvert.DeserializeObject<Meeting>(await response.Content.ReadAsStringAsync());

            ltiMeeting.Title = bjnMeeting.title;
            ltiMeeting.MeetingId = bjnMeeting.id;
            _providerContext.Entry(ltiMeeting).State = EntityState.Modified;
            _providerContext.SaveChanges();
            return RedirectToAction("Details", new { id});
        }

        // GET: BlueJeans/Tool/Delete/5
        public ActionResult Delete(int id)
        {
            var ltiRequest = GetLtiRequestFromClaim();
            if (ltiRequest == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid LTI request" });

            var consumer = _providerContext.Consumers.SingleOrDefault(c => c.Key.Equals(ltiRequest.ConsumerKey));
            if (consumer == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid Consumer" });

            var ltiMeeting = _providerContext.LtiMeetings.Find(id);
            if (ltiMeeting == null)
            {
                return RedirectToAction("BadRequest", "Error", new { error = "Meeting not found" });
            }

            return View(ltiMeeting);
        }

        // DELETE: BlueJeans/Tool/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var ltiRequest = GetLtiRequestFromClaim();
            if (ltiRequest == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid LTI request" });

            var consumer = _providerContext.Consumers.SingleOrDefault(c => c.Key.Equals(ltiRequest.ConsumerKey));
            if (consumer == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid Consumer" });

            var email = User.Identity.GetUserName();
            var appUser = _applicationUserManager.GetUsers().FirstOrDefault(x => x.Email == email);

            if (appUser == null)
            {
                return RedirectToAction("BadRequest", "Error", new { error = "User not found" });
            }

            var ltiMeeting = _providerContext.LtiMeetings.Find(id);

            await _apiClient.GetToken(appUser);
            await _apiClient.CancelMeeting(ltiMeeting.MeetingId);

            _providerContext.LtiMeetings.Remove(ltiMeeting);
            _providerContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Videos(int id, string numericMeetingId)
        {
            var ltiRequest = GetLtiRequestFromClaim();
            if (ltiRequest == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid LTI request" });

            var consumer = _providerContext.Consumers.SingleOrDefault(c => c.Key.Equals(ltiRequest.ConsumerKey));
            if (consumer == null)
                return RedirectToAction("BadRequest", "Error", new { error = "Invalid Consumer" });

            var ltiMeeting = _providerContext.LtiMeetings.Find(id);
            var meetingUser = _applicationUserManager.GetUsers().FirstOrDefault(x => x.Email == ltiMeeting.TeacherEmail);

            await _apiClient.GetToken(meetingUser);
            var videoUrlsResponse = await _apiClient.GetDownloadUrls(numericMeetingId);
            var videoUrls =
                JsonConvert.DeserializeObject<string[]>(await videoUrlsResponse.Content.ReadAsStringAsync());

            return View(videoUrls);
        }

        private LtiRequest GetLtiRequestFromClaim()
        {
            var user = User.Identity as ClaimsIdentity;
            if (user == null) return null;

            var claim = user.Claims.SingleOrDefault(c => c.Type.Equals("ProviderRequestId"));
            if (claim == null) return null;

            int providerRequestId;
            if (!int.TryParse(claim.Value, out providerRequestId)) return null;

            var providerRequest = _providerContext.ProviderRequests.Find(providerRequestId);
            if (providerRequest == null) return null;

            return JsonConvert.DeserializeObject<LtiRequest>(providerRequest.LtiRequest);
        }
    }
}