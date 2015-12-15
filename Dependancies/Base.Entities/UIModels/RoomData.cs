using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
namespace Base.Entities.UIModels
{
    public class RoomData
    {
        public RoomData()
        {
            RoomUsers = new List<ChatUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageFile { get; set; }   
        [ScriptIgnore]
        public List<ChatUser> RoomUsers { get; set; }
        public int UsersCount { get; set; }
        public string Tittle { get; set; }
        public string WelcomeMessage { get; set; }
    }
}
