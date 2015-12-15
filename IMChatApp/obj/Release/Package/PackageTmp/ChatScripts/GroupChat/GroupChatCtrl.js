
//(function () {  
//    'use strict'
    //  appApp = angular.module("GroupApp");
// grpChatController
//divRoomsList
//divPrivateList
//divRoomsInList
//divContacts
     appApp.controller("grpChatController", function ($scope , $rootScope, signalR, $compile) {

        console.log("load");
        // declear variables
        $scope.rooms = [];// RoomFactory.Rooms;
        //$scope.$parent.UserName = $("div#userId").text();
        $scope.users = [];
        $scope.me = $("div#userId").text();
        $scope.contextName = 'test';
        $scope.connectionId = '';
        $scope.messages = [];
        $scope.activeRoom = '';
        $scope.roomsLoggedIn = {};
        // end variable declearation
        // ServerSide methods//
        signalR.startHub();
        signalR.JoinChat();

        
        signalR.GetOnlineUsers(function (users)
        {
            console.log(users);
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
            console.log(rooms);
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