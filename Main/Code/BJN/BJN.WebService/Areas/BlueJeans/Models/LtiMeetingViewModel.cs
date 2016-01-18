using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BJN.Domain.Entities;
using Newtonsoft.Json;

namespace BJN.WebService.Areas.BlueJeans.Models
{
    public class LtiMeetingViewModel
    {
        public LtiMeetingViewModel()
        {
            advancedMeetingOptions = new LtiAdvancedMeetingOptionsViewModel();
        }

        public int LtiMeetingId { get; set; }
        public int id { get; set; }
        [Display(Name = "Class Title")]
        public string title { get; set; }
        [AllowHtml]
        [Display(Name = "Message")]
        public string description { get; set; }
        [Display(Name = "Start Time")]
        public DateTime startDateTime { get; set; }
        [Display(Name = "Length of Class in Minutes")]
        public int lengthOfClass { get; set; }
        public long start { get; set; }
        public long end { get; set; }
        public string timezone { get; set; }
        public LtiRecurrencePatternViewModel recurrencePattern { get; set; }
        public LtiAdvancedMeetingOptionsViewModel advancedMeetingOptions { get; set; }
        public string numericMeetingId { get; set; }
        public string attendeePasscode { get; set; }
        [Display(Name = "Add Attendee Passcode")]
        [DataType("YesNo")]
        public bool addAttendeePasscode { get; set; }
        public bool deleted { get; set; }
        public bool allow720p { get; set; }
        public string status { get; set; }
        public bool locked { get; set; }
        public int sequenceNumber { get; set; }
        public string icsUid { get; set; }
        public string endPointType { get; set; }
        public string endPointVersion { get; set; }
        [Display(Name = "Invite Students (comma separated list of email addresses)")]
        public string attendeesString { get; set; }
        public List<Attendee> attendees { get; set; }
        public bool isLargeMeeting { get; set; }
        public Dictionary<string, string> recurrencePatternValues { get; set; }
        public string durationString { get; set; }
        public long nextStart { get; set; }
        public long nextEnd { get; set; }
    }

    public class LtiRecurrencePatternViewModel
    {
        public string recurrenceType { get; set; }
        public long? endDate { get; set; }
        public int recurrenceCount { get; set; }
        public int frequency { get; set; }
        public int daysOfWeekMask { get; set; }
        public int dayOfMonth { get; set; }
        public string weekOfMonth { get; set; }
        public string monthOfYear { get; set; }
    }

    public class LtiAdvancedMeetingOptionsViewModel
    {
        public bool videoBestFit { get; set; }
        public bool publishMeeting { get; set; }
        public string encryptionType { get; set; }
        [Display(Name = "Moderator-less Class")]
        [DataType("YesNo")]
        public bool moderatorLess { get; set; }
        public bool allowStream { get; set; }
        [Display(Name = "Enable Auto-Recording")]
        [DataType("YesNo")]
        public bool autoRecord { get; set; }
        [Display(Name = "Disable Chat Messaging")]
        [DataType("YesNo")]
        public bool disallowChat { get; set; }
        [Display(Name = "Mute Participants on Entry")]
        [DataType("YesNo")]
        public bool muteParticipantsOnEntry { get; set; }
        [Display(Name = "Show participant names in email invitation")]
        [DataType("YesNo")]
        public bool showAllAttendeesInMeetingInvite { get; set; }
    }
}