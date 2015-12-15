using Base.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.Entities.UIModels
{
    public class ChatUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }
        public int UserType { get; set; }
        public string ConnectionId { get; set; }
        public string ContextName { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }        
        public string   Avatar { get; set; }
        public Status Status { get; set; }
    }
}
