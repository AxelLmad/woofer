using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class Message
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public DateTime Date { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}
