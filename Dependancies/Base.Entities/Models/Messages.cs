using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entities.Models
{
 public class Messages
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int fromUserId { get; set; }
        public int ToUserId { get; set; }
        public bool Status { get; set; }
        public byte[] CreatedTimeStamp { get; set; }
        public DateTime CreatedOn { get; set; }
        public Group group { get; set; }
    }
}
