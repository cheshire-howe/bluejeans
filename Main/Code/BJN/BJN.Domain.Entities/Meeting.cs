using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BJN.Domain.Entities
{
    public class Meeting
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public long start { get; set; }
        public long end { get; set; }
        public string timezone { get; set; }
        public RecurrencePattern recurrencePattern { get; set; }
        public AdvancedMeetingOptions advancedMeetingOptions { get; set; }
        public Moderator moderator { get; set; }
        public string numericMeetingId { get; set; }
        public string attendeePasscode { get; set; }
        public bool addAttendeePasscode { get; set; }
        public bool deleted { get; set; }
        public bool allow720p { get; set; }
        public string status { get; set; }
        public bool locked { get; set; }
        public int sequenceNumber { get; set; }
        public string icsUid { get; set; }
        public string endPointType { get; set; }
        public string endPointVersion { get; set; }
        public List<Attendee> attendees { get; set; }
        public bool isLargeMeeting { get; set; }
        public long created { get; set; }
        public long lastModified { get; set; }
        public bool isExpired { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public First first { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Last last { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Next next { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long nextStart { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long nextEnd { get; set; }
    }

    public class RecurrencePattern
    {
        public string recurrenceType { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? endDate { get; set; }
        public int recurrenceCount { get; set; }
        public int frequency { get; set; }
        public int daysOfWeekMask { get; set; }
        public int dayOfMonth { get; set; }
        public string weekOfMonth { get; set; }
        public string monthOfYear { get; set; }
    }

    public class Attendee
    {
        public int id { get; set; }
        public MeetingAttending meeting { get; set; }
        public string email { get; set; }
        public object followupEmailSentDate { get; set; }
    }

    public class MeetingAttending
    {
        public int id { get; set; }
    }

    public class AdvancedMeetingOptions
    {
        public bool videoBestFit { get; set; }
        public bool publishMeeting { get; set; }
        public string encryptionType { get; set; }
        public bool moderatorLess { get; set; }
        public bool allowStream { get; set; }
        public bool autoRecord { get; set; }
        public bool disallowChat { get; set; }
        public bool muteParticipantsOnEntry { get; set; }
        public bool showAllAttendeesInMeetingInvite { get; set; }
    }

    public class Moderator
    {
        public int id { get; set; }
        public string username { get; set; }
    }

    public class First
    {
        public long start { get; set; }
        public long end { get; set; }
    }

    public class Last
    {
        public long start { get; set; }
        public long end { get; set; }
    }

    public class Next
    {
        public long start { get; set; }
        public long end { get; set; }
    }
}
