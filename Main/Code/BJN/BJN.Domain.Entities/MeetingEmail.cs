using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJN.Domain.Entities
{
    public class MeetingEmail
    {
        public ModeratorEmail moderator { get; set; }
        public ParticipantEmail participant { get; set; }
    }

    public class ModeratorEmail
    {
        public Create create { get; set; }
        public Update update { get; set; }
        public Delete delete { get; set; }
    }

    public class ParticipantEmail
    {
        public Inmeeting inmeeting { get; set; }
        public Create create { get; set; }
        public Update update { get; set; }
        public Delete delete { get; set; }
    }

    public class EmailSection
    {
        public string ICS { get; set; }
        public string Text { get; set; }
        public string HTML { get; set; }
        public string ICS_FILE { get; set; }
    }

    public class Inmeeting : EmailSection
    {
    }

    public class Create : EmailSection
    {
    }

    public class Update : EmailSection
    {
    }

    public class Delete : EmailSection
    {
    }
}
