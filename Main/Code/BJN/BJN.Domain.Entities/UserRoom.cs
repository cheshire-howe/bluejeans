using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Domain.Entities
{
    public class UserRoom
    {
        public int id { get; set; }
        public string name { get; set; }
        public string numericId { get; set; }
        public int personalMeetingId { get; set; }
        public string moderatorPasscode { get; set; }
        public string participantPasscode { get; set; }
        public string backgroundImage { get; set; }
        public string defaultLayout { get; set; }
        public string welcomeMessage { get; set; }
        public int originPopId { get; set; }
        public bool allow720p { get; set; }
        public bool playAudioAlerts { get; set; }
        public bool showVideoAnimations { get; set; }
        public bool publishMeeting { get; set; }
        public string encryptionType { get; set; }
        public bool videoBestFit { get; set; }
        public bool showAllParticipantsInIcs { get; set; }
        public bool isModeratorLess { get; set; }
        public int idleTimeout { get; set; }
        public bool disallowChat { get; set; }
        public bool muteParticipantsOnEntry { get; set; }
        public bool isLargeMeeting { get; set; }
    }
}
