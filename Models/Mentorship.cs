using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.Models
{
    public class Mentorship
    {
        public int MentorshipId { get; set; }

        public string StudentID { get; set; }

        public string MentorID { get; set; }

        //List<Goal> Goals { get; set; }
    }
}
