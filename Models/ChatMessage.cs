using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCompanionApp.Models
{
    public class ChatMessage
    {
        public int ChatMessageId { get; set; }
        public int ThreadId { get; set; }
        public string Content { get; set; }
        public string SenderId { get; set; }
        public DateTime MessageTimeStamp { get; set; }
    }

    public class ChatThread
    {
        public int ThreadId { get; set; }

        public List<ChatMessage> ChatMessages { get; set; }
    }


}
