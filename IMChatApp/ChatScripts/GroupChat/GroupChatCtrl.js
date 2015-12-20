
//(function () {  
//    'use strict'
    //  appApp = angular.module("GroupApp");
// grpChatController
//divRoomsList
//divPrivateList
//divRoomsInList
//divContacts

appApp.controller("grpChatController", function ($scope, $rootScope, signalR, $compile, $filter) {
         console.log("load");
         $scope.ShowRoom = true;
        // declear variables
        $scope.rooms = [];// RoomFactory.Rooms;
        //$scope.$parent.UserName = $("div#userId").text();
        $scope.users = [];
        $scope.me = {} ;// $("div#userId").text();
        $scope.contextName = 'test';
        $scope.connectionId = '';
        $scope.messages = [];
        $scope.activeRoom = '';
        $scope.roomsLoggedIn = [];
        $scope.roomUsers = [];
        $scope.usersInPrivateChat = [];
        $scope.privateMessages = [];
        $scope.roomMessages = [];   
        // end variable declearation
        // ServerSide methods//
        signalR.startHub();
        signalR.JoinChat();        
        signalR.GetOnlineUsers(function (users)
        {
            //console.log(users);
            $scope.users = formatUser(JSON.parse(users));
            //console.log($scope.users);
            $scope.$apply();
        });
        signalR.AddNewUser(function (user) { console.log("new user addeed"); console.log(user);});
        signalR.GetRoomUsers(function (users, room) {
            //console.log(users);          
            $scope.usersInCurrentRoom = formatUser(JSON.parse(users));
            $scope.roomUsers.push(formatRoomUsers(room, users));
            //console.log($scope.roomUsers);
            $scope.$apply();
        });
        //signalR.UserEntered(function (room, user, cid) {
        //    if ($scope.activeRoom == room && user != '') {
        //        var result = $.grep($scope.users, function (e) { return e.name == user; })
        //         if (result != undefined || result != null) {
        //            $scope.users.push({ name: user, ConnectionId: cid });
        //            $scope.$apply();
        //        }
        //    }
        //});
        signalR.GetRooms(function (rooms, me) { //console.log(rooms);
            $scope.rooms = formatRoom(JSON.parse(rooms));
          //  console.log($scope.rooms);   // console.log(me);
            $scope.me = CreateUser(me);
            console.log($scope.me);
        });
        signalR.UserLoggedOut(function (room, user) {
            if ($scope.activeRoom == room && user != '') {
                $scope.users = $scope.users.filter(function (themObjects) {
                    return themObjects.name != user;
                });
                $scope.$apply();
            }
        });         
        //clients methods
        function initializeNewRoom(room)
        {
            var headerHtml = ' <li class=""><a href="# ' + room + '">Lobby</a></li>  ';
            $("#ChatRoomsTab").append(headerHtml);
            var Content = '<div class="tab-pane active" id="'+room+'"> </div>';
            $("#ChatRoomsTabContent").append(Content);
            //load users for the room
        }
        $scope.roomClicked = function (id) {
           // console.log(id + "clicked");
            signalR.JoinRoom(id);
            var found = $filter('filter')($scope.roomsLoggedIn, { id: id }, true);
            if (!found.length) {
                $scope.roomsLoggedIn.push($filter('filter')($scope.rooms, { id: id }, true)[0]); // = JSON.stringify(found[0]);
            } else {
               // $scope.selected = 'Not found';
            }
            // $scope.$apply();
          //  console.log($scope.roomsLoggedIn);
        }
        $scope.roomClosed = function (r) {
           // console.log(r);
            $scope.roomsLoggedIn.splice($scope.roomsLoggedIn.indexOf(r), 1);
        }
        $scope.openePrivateChat = function (id) {
          //  signalR.JoinPrivateChat(id);
            var found = $filter('filter')($scope.usersInPrivateChat, { id: id }, true);
            if (!found.length) {
                $scope.usersInPrivateChat.push($filter('filter')($scope.users, { id: id }, true)[0]); // = JSON.stringify(found[0]);
            } else {
                // $scope.selected = 'Not found';
                console.log('exists');
            }
            //$scope.$applasy();
            //console.log($scope.usersInPrivateChat);
        }
        $scope.closePrivateChat = function (u) {
            console.log(u);
            $scope.usersInPrivateChat.splice($scope.usersInPrivateChat.indexOf(u), 1);
        }

        $scope.showRoomList = function ()
        {
            $(".selectList").toggleClass("on");
            $scope.ShowRoom = true;
        }
        $scope.showUserList = function () {
            $(".selectList").toggleClass("on");
            $scope.ShowRoom = false;
        }
        $scope.sendRoomMessage = function (toroom, from, message)
        {
            $scope.roomMessages.push(formatMessage(toroom, from, avatar, message, 'room', status))
        }
        $scope.sendPvtMessage = function (toUser, user, message)
        {
            $scope.privateMessages.push(formatMessage(to, user.name, avatar, message, 'private', status));
        }
   // JoinRoom: function (id) {
        //
        function loadAvailableRooms()
        {
           // $.each()

            //<li>
            //               <a href="#">
            //                   <img class="avatar" alt="Lucas" src="~/Content/img/avatar.jpg">
            //               </a>
            //               <strong>Name:</strong> <a href="#">Łukasz Holeczek</a><br>
            //               <strong>Since:</strong> Jul 25, 2012 11:09<br>
            //               <strong>Status:</strong> <span class="label label-success">Approved</span>
            //           </li>

        }

    })
//});
function formatMessage(to, from,avatar,message,type,status)
{
    d = new Date();
    //d.toLocaleString();       // -> "2/1/2013 7:37:08 AM"
    //d.toLocaleDateString();   // -> "2/1/2013"
    //d.toLocaleTimeString();
    return {
        to: to,
        from: from,
        avatar:avatar,
        message: message,
        type: type,
        unread: status,
        time:d.toLocaleTimeString()
    }

}

     function formatUser(users) {
         if (!users || users.length === 0) {
             return null;
         }
         return users.map(function (user) { return CreateUser(user);});
         //{
         //    if (user.Avatar == '' || user.Avatar=='normal')
         //        user.Avatar = 'avatar4.jpg';
         //    return CreateUser(user); 
             // {
             //    name: user.Name,
             //    avatar: user.Avatar,
             //    id: user.Id,
             //    connectionId:user.ConnectionId,
             //    age: user.Age,
             //    nick: user.Nick,
             //    status: 'online',
             //    sub: user.Name.substring(0, 1),
             //    unreadmessage: 0
             //};

        // });
     }
  function CreateUser(user){
         if (user.Avatar == '' || user.Avatar=='normal')
             user.Avatar = 'avatar4.jpg';
         return {
             name: user.Name,
             avatar: user.Avatar,
             id: user.Id,
             connectionId:user.ConnectionId,
             age: user.Age,
             nick: user.Nick,
             status: 'online',
             sub: user.Name.substring(0, 1),
             unreadmessage: 0
         };
     
     }

     function formatRoom(rooms) {
         if (!rooms || rooms.length === 0) {
             return null;
         }
         //debugger;
         return rooms.map(function (room) {
             return {
                 name: room.Name,
                 avatar: room.ImageFile, // getAvator('room',room.name,room.avator),
                 id: room.Id,
                 title: room.Tittle,
                 status: 'offline',
                 sub: room.Name.substring(0, 1),
                 users: room.UsersCount,
                 welcomeMessage:room.WelcomeMessage,
                 unreadmessage: 0
             };
         });
     }
     
     function formatRoomUsers(room, users) {
         //if (!rooms || rooms.length === 0) {
         //    return null;
         //}
         //debugger;       
             return {
                 name: room.Name,               
                 id: room.Id,               
                 status:true,
                 sub: room.Name.substring(0, 1),
                 users: users,              
                 unreadmessage: 0
             };
         
     }

     function getAvator(avatarFor, name, avatar)
     {
         if (avatar != '')
             return avatar;
         var char = room.name.substring(0, 1);
         if (avatarFor == 'room')
             return '';
         else 
             return '';
     }
     