

namespace MentorshipCompanionApp.Models
{
    public class Goal
    {
        public int GoalId { get; set; }

        public int? MentorshipId { get; set; }
        public string StudentId { get; set; }

        public string GoalName { get; set; }
        public string GoalDescription { get; set; } = string.Empty;

        public DateTime? GoalDate { get; set; }
        public DateTime? Deadline { get; set; }

        public string Status { get; set; }

        public ChatThread? GoalThread { get; set; }
    }
}
