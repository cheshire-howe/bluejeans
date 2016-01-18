using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJN.Domain.Entities;

namespace BJN.Services.Connectors.Lti.Models
{
    public class LtiMeeting
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int MeetingId { get; set; }
        [Required]
        [StringLength(64)]
        public string CourseId { get; set; }

        public string TeacherEmail { get; set; }

        public virtual Consumer Consumer { get; set; }
    }
}
