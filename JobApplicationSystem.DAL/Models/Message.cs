using System;
using System.Collections.Generic;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public string? Sender { get; set; }
        public string? Receiver { get; set; }
        public string? Content { get; set; }
        public DateTime? Date { get; set; }

        public virtual AspNetUser? ReceiverNavigation { get; set; }
        public virtual AspNetUser? SenderNavigation { get; set; }
    }
}
