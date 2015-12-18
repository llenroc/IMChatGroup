
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
        $scope.me = $("div#userId").text();
        $scope.contextName = 'test';
        $scope.connectionId = '';
        $scope.messages = [];
        $scope.activeRoom = '';
        $scope.roomsLoggedIn = [];
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
        //signalR.UserEntered(function (room, user, cid) {
        //    if ($scope.activeRoom == room && user != '') {
        //        var result = $.grep($scope.users, function (e) { return e.name == user; })
        //         if (result != undefined || result != null) {
        //            $scope.users.push({ name: user, ConnectionId: cid });
        //            $scope.$apply();
        //        }
        //    }
        //});
        signalR.GetRooms(function (rooms, me) {
            //console.log(rooms);
            $scope.rooms = formatRoom(JSON.parse(rooms));
          //  console.log($scope.rooms);
            console.log(me);
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
            console.log(id + "clicked");
            
            var found = $filter('filter')($scope.roomsLoggedIn, { id: id }, true);
            if (!found.length) {
                $scope.roomsLoggedIn.push($filter('filter')($scope.rooms, { id: id }, true)[0]); // = JSON.stringify(found[0]);
            } else {
               // $scope.selected = 'Not found';
            }
            // $scope.$apply();
            console.log($scope.roomsLoggedIn);
        }
        $scope.roomClosed = function (r) {
            console.log(r);
            $scope.roomsLoggedIn.splice($scope.roomsLoggedIn.indexOf(r), 1);
        }
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


     function formatUser(users) {
         if (!users || users.length === 0) {
             return null;
         }
         return users.map(function (user) {
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

         });
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
     
     function getAvator(avatorFor, name, avator)
     {
         if (avator != '')
             return avator;
         var char = room.name.substring(0, 1);
         if (avatorFor == 'room')
             return '';
         else 
             return '';
     }
     