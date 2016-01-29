using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Base.Entities.UIModels;
using Base.Entities.Models;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
namespace IMChatApp.Hubs
{
    [HubName("srchat")]
    public class SRChat : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
      public  static List<ChatUser> chatUsers = new List<ChatUser>();
        static List<RoomData> Rooms = new List<RoomData>();
        public void InitializeChat()
        {
            Rooms.Add(new RoomData { Id = 1, Name = "Rooms 1", Tittle = "Room 1" });
            Rooms.Add(new RoomData { Id = 2, Name = "Rooms 2", Tittle = "Room 2" });
            Rooms.Add(new RoomData { Id = 3, Name = "Rooms 3", Tittle = "Room 3" });
            Rooms.Add(new RoomData { Id = 4, Name = "Rooms 4", Tittle = "Room 4" });
            Rooms.Add(new RoomData { Id = 5, Name = "Rooms 5", Tittle = "Room 5" });
            Rooms.Add(new RoomData { Id = 6, Name = "Rooms 6", Tittle = "Room 6" });
            getDummyUsers();
        }

        public void JoinChatOld(string nick)
        {
            if (Rooms.Count == 0)
                InitializeChat();
           // var a = HttpContext.Current.User.Identity.Name;
           // var user = chatUsers.Where(x => x.Nick == nick.Trim()).FirstOrDefault();
            if (nick != string.Empty)
            {
              // chatUsers.Where(x => x.Nick == nick.Trim()).FirstOrDefault()=
                var user = new ChatUser
                {
                    Name = nick,
                    ConnectionId = Context.ConnectionId,
                    ContextName = nick,
                    Nick=nick,
                    Age = 20,
                    Avatar = "",
                    Id = 1,
                    Gender = Gender.Male,
                    UserType = Convert.ToInt32(UserType.Guest), //fontColor = "red", 
                    Status = Status.Active
                };
                // Clients.Caller.setInitial(Context.ConnectionId, a);
                var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string sJSON = oSerializer.Serialize(chatUsers);

                Clients.Caller.getrooms(oSerializer.Serialize(Rooms), user);
                if (!(chatUsers.Where(u => u.ConnectionId == Context.ConnectionId).Count() > 0))
                    chatUsers.Add(user);
                Clients.Caller.getOnlineUsers(sJSON);
                Clients.Others.newOnlineUser(user);
            }
        }



        public void JoinRoom(int id)
        {
            if (Rooms.Where(r => r.Id == id).FirstOrDefault().RoomUsers.Where(u => u.ConnectionId == Context.ConnectionId).Count() == 0)
            {
                var users = chatUsers.Where(u => u.ConnectionId == Context.ConnectionId);
                var user =users.FirstOrDefault();
                if (user != null)
                {
                    Rooms.Where(x => x.Id == id).FirstOrDefault()
                        .RoomUsers.Add(user);
                    Groups.Add(Context.ConnectionId, id.ToString());
                    Rooms.Where(x => x.Id == id).FirstOrDefault().UsersCount++;
                    Clients.All.addNewUser(user, id); //.Group(id.ToString())
                    var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Clients.Caller.getRoomUsers(oSerializer.Serialize(Rooms.Where(x => x.Id == id).FirstOrDefault().RoomUsers), Rooms.Where(x => x.Id == id).FirstOrDefault());
                }
            }
        }
        public void JoinPrivateChat(int id) { 
      //  Clients.Caller.
        
        }
        public void LeaveRoom(int id)
        {
            ChatUser user = chatUsers.Where(u => u.ConnectionId == Context.ConnectionId).FirstOrDefault();
             //   || u.ContextName == HttpContext.Current.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                Rooms.Where(x => x.Id == id).FirstOrDefault().RoomUsers.Remove(user);
                Rooms.Where(x => x.Id == id).FirstOrDefault().UsersCount--;
            }
            // .RoomUsers.Remove(user);
            //chatUsers.Where(x=>x.)
            Groups.Remove(Context.ConnectionId, id.ToString());
            Clients.Caller.loggedOutRoom(user.Id, id);//(user,id);
            Clients.Others.leftRoom(user.Id, id);
        }


        public void SendMessage(string message,int id ,bool isPvt )
        {
            if (message.Contains("<script>"))
            {
                throw new HubException("This message will flow to the client", new { user = Context.User.Identity.Name, message = message });
            }
            var sender = chatUsers.Where(u => u.ConnectionId == Context.ConnectionId).SingleOrDefault();
            if (isPvt){
                var user=chatUsers.Where(x=>x.Id==id).FirstOrDefault();
                Clients.Client(user.ConnectionId).recivePrivateMessage(user,sender, message);
            }
            else {
                Clients.OthersInGroup(id.ToString()).reciveRoomMessage(id, sender, message);
            }        
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            ChatUser item = chatUsers.Where(u => u.ConnectionId == Context.ConnectionId).FirstOrDefault();
            if (item != null)
            {
                var rooms= Rooms.Where(r=>r.RoomUsers.Contains(item)) ;
               // loggedInUsers.Remove(item); // list = 
                var id = Context.ConnectionId;
                Clients.Others.newOfflineUser(item);
               // var groups=Groups.
               
                foreach(var r in rooms)
                {
                    Clients.Others.LeftRoom(item.Id, r.Id);
                    Groups.Remove(item.ConnectionId,r.Id.ToString());
                    Rooms.Where(x => x.Id == r.Id).FirstOrDefault().RoomUsers.Remove(item);
                }
                Clients.Others.userLoggedOff(item.Id);
                chatUsers.Remove(item);
            }
            return base.OnDisconnected(stopCalled);
        }

        //public override Task OnConnected()
        //{
        //    ChatUser item = chatUsers.Where(u => u.ConnectionId == Context.ConnectionId).FirstOrDefault();
        //    if (item == null)
        //    {
        //        var sessionId = Context.Headers["Referer"].ToString().Split('=')[1].Trim();//   	="http://localhost:8887/Metro/chat?gid=0a63df3a-cbfa-4d98-992c-628e83f3b7f5
        //        item = chatUsers.Where(u => u.SessionId.ToString() == sessionId).FirstOrDefault();           
        //        if (item != null && item.ConnectionId!=null)
        //        {
        //            var oldOcnnectionId = item.ConnectionId;
        //            chatUsers.Where(u => u.SessionId.ToString() == sessionId).FirstOrDefault().ConnectionId = Context.ConnectionId;
        //            var rooms = Rooms.Where(r => r.RoomUsers.Contains(item));
        //            foreach (var r in rooms)
        //            {
        //                Clients.OthersInGroup(r.Id.ToString()).userLoggedOff(item.Id, r.Id);
        //                Groups.Remove(oldOcnnectionId, r.Id.ToString());
        //                Groups.Add(Context.ConnectionId, r.Id.ToString());
        //            }
                    
        //            if(true){
                    
        //            }
        //        }
        //    }
        //   // AddGroups();
        //    return base.OnConnected();
        //}

        ////rejoin groups if client disconnects and then reconnects
        //public override Task OnReconnected()
        //{
        //   // AddGroups();
        //    ChatUser item = chatUsers.Where(u => u.ConnectionId == Context.ConnectionId).FirstOrDefault();
        //    if (item == null)
        //    {
        //        var sessionId = Context.Headers["Referer"].ToString().Split('=')[1].Trim();//   	="http://localhost:8887/Metro/chat?gid=0a63df3a-cbfa-4d98-992c-628e83f3b7f5
        //        item = chatUsers.Where(u => u.SessionId.ToString() == sessionId).FirstOrDefault();
        //        if (item != null && item.ConnectionId != null)
        //        {
        //            var oldOcnnectionId = item.ConnectionId;
        //            chatUsers.Where(u => u.SessionId.ToString() == sessionId).FirstOrDefault().ConnectionId = Context.ConnectionId;
        //            var rooms = Rooms.Where(r => r.RoomUsers.Contains(item));
        //            foreach (var r in rooms)
        //            {
        //                Clients.OthersInGroup(r.Id.ToString()).userLoggedOff(item.Id, r.Id);
        //                Groups.Remove(oldOcnnectionId, r.Id.ToString());
        //                Groups.Add(Context.ConnectionId, r.Id.ToString());
        //            }

        //            if (true)
        //            {

        //            }
        //        }
        //    }
        //    return base.OnReconnected();
        //}
        public void getDummyUsers()
        {
           // var connectionid = Context.ConnectionId;
            for (int i = 0; i < 15; i++)
            {
                chatUsers.Add(new ChatUser
                {
                    Id = i,
                    ContextName = "Context" + i,
                    Gender = Gender.Male,
                    ConnectionId = Guid.NewGuid().ToString(),//  connectionid.Substring(0, connectionid.Length - 2) + i,
                    Status = Status.Active,
                    UserType = 1,
                    Avatar = "normal",
                    Age = 15 + i,
                    Name = "user" + i,
                    Nick = "user-" + i
                });
            }
            Rooms.Where(x => x.Id == 1).SingleOrDefault().RoomUsers.AddRange(chatUsers.Where(u => u.Id < 7));
            Rooms.Where(x => x.Id == 1).SingleOrDefault().UsersCount = Rooms.Where(x => x.Id == 1).SingleOrDefault().RoomUsers.Count();
            Rooms.Where(x => x.Id == 2).SingleOrDefault().RoomUsers.AddRange(chatUsers.Where(u => u.Id > 3 && u.Id < 12));
            Rooms.Where(x => x.Id == 2).SingleOrDefault().UsersCount = Rooms.Where(x => x.Id == 2).SingleOrDefault().RoomUsers.Count(); ;
            Rooms.Where(x => x.Id == 3).SingleOrDefault().RoomUsers.AddRange(chatUsers.Where(u => u.Id > 6 && u.Id < 15));
            Rooms.Where(x => x.Id == 3).SingleOrDefault().UsersCount = Rooms.Where(x => x.Id == 3).SingleOrDefault().RoomUsers.Count(); ;
            Rooms.Where(x => x.Id == 4).SingleOrDefault().RoomUsers.AddRange(chatUsers.Where(u => u.Id > 2 && u.Id < 7));
            Rooms.Where(x => x.Id == 4).SingleOrDefault().UsersCount = Rooms.Where(x => x.Id == 4).SingleOrDefault().RoomUsers.Count(); ;
            Rooms.Where(x => x.Id == 5).SingleOrDefault().RoomUsers.AddRange(chatUsers.Where(u => u.Id >=6 ));
            Rooms.Where(x => x.Id == 5).SingleOrDefault().UsersCount = Rooms.Where(x => x.Id == 5).SingleOrDefault().RoomUsers.Count(); ;
        }

        public override Task OnConnected()
        {
            JoinChat(null);
            return base.OnConnected();
        }

        public void JoinChat(string sessionId)
        {
            if (sessionId == null)
            {
                var url=Context.Headers["Referer"].ToString();
                if (url.Contains("="))
                {
                    sessionId = url.Split('=')[1].Trim();// 
                }
                else
                {
                    return;
                }
            }
            if (Rooms.Count == 0)
                InitializeChat();
            // var a = HttpContext.Current.User.Identity.Name;
            var user = chatUsers.Where(x => x.SessionId.ToString() == sessionId.Trim()).FirstOrDefault();
            if (user != null)
            {
                user.ConnectionId = Context.ConnectionId;
                if (user.Id == 0)
                    user.Id = chatUsers.Max(x => x.Id) + 1;
                if (string.IsNullOrEmpty(user.Name))
                    user.Name = user.Nick;
                user.ContextName = user.Name;
                var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string sJSON = oSerializer.Serialize(chatUsers);
                Clients.Caller.getrooms(oSerializer.Serialize(Rooms), user);
                if (!(chatUsers.Where(u => u.ConnectionId == Context.ConnectionId).Count() > 0))
                    chatUsers.Add(user);
                Clients.Caller.getOnlineUsers(sJSON);
                Clients.Others.newOnlineUser(user);
            }
        }

        #region  Moderationg Users

        public void KickUserFromRoom(int roomId, int userId) {         
        
        }
        #endregion
    }


}