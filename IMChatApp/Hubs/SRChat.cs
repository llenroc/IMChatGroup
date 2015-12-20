using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Base.Entities.UIModels;
using Base.Entities.Models;
using Microsoft.AspNet.SignalR.Hubs;
namespace IMChatApp.Hubs
{
    [HubName("srchat")]
    public class SRChat : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        static List<ChatUser> chatUsers = new List<ChatUser>();
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

        public void JoinChat()
        {
            if (Rooms.Count == 0)
                InitializeChat();
            var a = HttpContext.Current.User.Identity.Name;
            if (a == string.Empty)
                a = "TestUser";
            var user = new ChatUser
            {
                Name = a,
                ConnectionId = Context.ConnectionId,
                ContextName = a,
                Age = 20,
                Avatar = "",
                Id = 1,
                Gender = Gender.Male,
                UserType = 1, //fontColor = "red", 
                Status = Status.Active
            };
          
           // Clients.Caller.setInitial(Context.ConnectionId, a);
            var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(chatUsers);

            Clients.Caller.getrooms(oSerializer.Serialize(Rooms), user);
            if (!(chatUsers.Where(u=>u.ConnectionId==Context.ConnectionId).Count()>0))
            chatUsers.Add(user);
            Clients.Caller.getOnlineUsers(sJSON);
            Clients.Others.newOnlineUser(user);
        }

        public void JoinRoom(int id)
        {
            if (Rooms.Where(r => r.Id == id).FirstOrDefault().RoomUsers.Where(u => u.ConnectionId == Context.ConnectionId).Count() == 0)
            {
                var user = chatUsers.Where(u => u.ConnectionId == Context.ConnectionId || u.ContextName == HttpContext.Current.User.Identity.Name).FirstOrDefault();

                Rooms.Where(x => x.Id == id).FirstOrDefault()
                    .RoomUsers.Add(user);
                Groups.Add(Context.ConnectionId, id.ToString());
                Clients.Group(id.ToString()).addNewUser(user);
                var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                Clients.Caller.getRoomUsers(oSerializer.Serialize(Rooms.Where(x => x.Id == id).FirstOrDefault().RoomUsers), Rooms.Where(x => x.Id == id).FirstOrDefault());
            }
        }
        public void JoinPrivateChat(int id) { 
      //  Clients.Caller.
        
        }
        public void LeaveRoom(int id)
        {
            var user = chatUsers.Where(u => u.ConnectionId == Context.ConnectionId
                || u.ContextName == HttpContext.Current.User.Identity.Name).FirstOrDefault();
            Rooms.Where(x => x.Id == id).FirstOrDefault()
                .RoomUsers.Remove(user);
            Groups.Remove(Context.ConnectionId, id.ToString());
            Clients.Group(id.ToString()).removeNewUser(user);
        }
        public void SendMessage(string message,int id ,bool isPvt )
        {
            var sender = chatUsers.Where(u => u.ConnectionId == Context.ConnectionId).SingleOrDefault();
            if (isPvt){
                var user=chatUsers.Where(x=>x.Id==id).FirstOrDefault();
                Clients.Client(user.ConnectionId).recivePrivateMessage(user,sender, message);
            }
            else {
                Clients.OthersInGroup(id.ToString()).reciveRoomMessage(id, sender, message);
            }        
        }
        public void getDummyUsers()
        {
            var connectionid = Context.ConnectionId;
            for (int i = 0; i < 15; i++)
            {
                chatUsers.Add(new ChatUser
                {
                    Id = i,
                    ContextName = "Context" + i,
                    Gender = Gender.Male,
                    ConnectionId = connectionid.Substring(0, connectionid.Length - 2) + i,
                    Status = Status.Active,
                    UserType = 1,
                    Avatar = "normal",
                    Age = 15 + i,
                    Name = "user" + i,
                    Nick = "user-" + i
                });
            }
            Rooms.Where(x => x.Id == 1).SingleOrDefault().RoomUsers.AddRange(chatUsers.Where(u => u.Id < 7));
            Rooms.Where(x => x.Id == 1).SingleOrDefault().UsersCount = 7;
            Rooms.Where(x => x.Id == 2).SingleOrDefault().RoomUsers.AddRange(chatUsers.Where(u => u.Id > 3 && u.Id < 12));
            Rooms.Where(x => x.Id == 2).SingleOrDefault().UsersCount = 7;
            Rooms.Where(x => x.Id == 3).SingleOrDefault().RoomUsers.AddRange(chatUsers.Where(u => u.Id > 6 && u.Id < 15));
            Rooms.Where(x => x.Id == 3).SingleOrDefault().UsersCount = 7;
            Rooms.Where(x => x.Id == 4).SingleOrDefault().RoomUsers.AddRange(chatUsers.Where(u => u.Id > 2 && u.Id < 7));
            Rooms.Where(x => x.Id == 4).SingleOrDefault().UsersCount = 5;
            Rooms.Where(x => x.Id == 5).SingleOrDefault().RoomUsers.AddRange(chatUsers.Where(u => u.Id >=6 ));
            Rooms.Where(x => x.Id == 5).SingleOrDefault().UsersCount = 7;
        }
    }
}