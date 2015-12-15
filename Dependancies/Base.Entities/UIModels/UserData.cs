using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Base.Entities.UIModels
{
   public class UserData
    {
       public UserData()
       {
           RoomsLoggedIn = new List<RoomData>();
           UsersInPrivatetChat = new List<ChatUser>();
           ChatFriends = new List<ChatUser>();
           ChatUser = new ChatUser();
       }
        public ChatUser ChatUser { get; set; }
        [ScriptIgnore]
        //[JsonIgnore]
        public List<RoomData> RoomsLoggedIn { get; set; }
        [ScriptIgnore]
        public List<ChatUser> UsersInPrivatetChat { get; set; }
        [ScriptIgnore]
        public List<ChatUser> ChatFriends { get; set; }
    }
}
