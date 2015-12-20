
//(function () {  
//    'use strict'
//  appApp = angular.module("GroupApp");
// grpChatController
//divRoomsList
//divPrivateList
//divRoomsInList
//divContacts

appApp.controller("grpChatController", function ($scope, $rootScope, signalR, $compile, $filter) {  
    $scope.ShowRoom = true;
    // declear variables
    $scope.rooms = [];// RoomFactory.Rooms;
    //$scope.$parent.UserName = $("div#userId").text();
    $scope.users = [];
    $scope.me = {};// $("div#userId").text();
    $scope.contextName = 'test';
    $scope.connectionId = '';
    $scope.message = '';
    $scope.activeRoom = '';
    $scope.roomsLoggedIn = [];
    $scope.roomUsers = [];
    $scope.usersInPrivateChat = [];
    $scope.privateMessages = [];
    $scope.roomMessages = [];
    $scope.to = {};
    // end variable declearation
    // ServerSide methods//
    signalR.startHub();
    signalR.JoinChat();
    signalR.GetOnlineUsers(function (users) {
        //console.log(users);
        $scope.users = formatUser(JSON.parse(users));
        //console.log($scope.users);
        $scope.$apply();
    });
    signalR.AddNewUser(function (user) {
        //console.log("new user addeed"); console.log(user);
    });
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
            //console.log($scope.me);
        });
        signalR.UserLoggedOut(function (room, user) {
            if ($scope.activeRoom == room && user != '') {
                $scope.users = $scope.users.filter(function (themObjects) {
                    return themObjects.name != user;
                });
                if (!$scope.$$phase) {
                    $scope.$apply();
                }
            }
        });
        signalR.RecivePrivateMessage(function (user, sender, message) {
            var to = { id: id, type: 'private' };
            var from = CreateUser(sender);
            $scope.privateMessages.push(formatMessage(to, from, message, false));
            if (!$scope.$$phase) {
                $scope.$apply();
            }
        });
        signalR.ReciveRoomMessage(function (id, sender, message) {
            console.log(id + message);
            var from = CreateUser(sender);
            var to = { id: id, type: 'room' };
            $scope.roomMessages.push(formatMessage(to, from, message, false));
            if (!$scope.$$phase) {
                $scope.$apply();
            }
       
        });
        //clients methods
        function initializeNewRoom(room) {
            var headerHtml = ' <li class=""><a href="# ' + room + '">Lobby</a></li>  ';
            $("#ChatRoomsTab").append(headerHtml);
            var Content = '<div class="tab-pane active" id="' + room + '"> </div>';
            $("#ChatRoomsTabContent").append(Content);
            //load users for the room
        }
        $scope.roomClicked = function (id) {
            if (!($scope.to.id == id && $scope.to.type == 'room')) {
                $scope.to = { id: id, type: 'room' };
                // console.log(id + "clicked");
                signalR.JoinRoom(id);
                var found = $filter('filter')($scope.roomsLoggedIn, { id: id }, true);
                if (!found.length) {
                    $scope.roomsLoggedIn.push($filter('filter')($scope.rooms, { id: id }, true)[0]); // = JSON.stringify(found[0]);
                } else {
                    // $scope.selected = 'Not found';
                }          
            }
            else {
                $scope.messagesToShow = $filter('filter')($scope.roomMessages, { to: id }, true);
            }
        }
        $scope.roomClosed = function (r) {
            debugger;
            $.each($scope.roomMessages, function (i) {
                if ($scope.roomMessages[i].to == r.id) {
                    $scope.roomMessages.splice(i, 1);
                    return false;
                }
            });
            // console.log(r);
            $scope.roomsLoggedIn.splice($scope.roomsLoggedIn.indexOf(r), 1);
            if ($scope.roomsLoggedIn.length != 0) {
                var last = $scope.roomsLoggedIn[$scope.roomsLoggedIn.length - 1]
                $scope.messagesToShow = $filter('filter')($scope.roomMessages, { to: last.id }, true);
                $scope.to = { id: last.id, type: 'room' };
            }
            else {
                if ($scope.usersInPrivateChat.length != 0) {
                    var last = $scope.usersInPrivateChat[$scope.usersInPrivateChat.length - 1];
                    $scope.to = { id: last.id, type: 'private' };
                    $scope.messagesToShow = $filter('filter')($scope.privateMessages, { to: last.id }, true);
                }
                else { $scope.messagesToShow = [];}
            }
        }

        $scope.openePrivateChat = function (id) {
            debugger;
            if (!($scope.to.id == id && $scope.to.type == 'private')) {
                $scope.to = { id: id, type: 'private' };
                //  signalR.JoinPrivateChat(id);
                var found = $filter('filter')($scope.usersInPrivateChat, { id: id }, true);
                if (!found.length) {
                    $scope.usersInPrivateChat.push($filter('filter')($scope.users, { id: id }, true)[0]); // = JSON.stringify(found[0]);
                } else {
                    // $scope.selected = 'Not found';
                    console.log('exists');
                }          
            }
            else
            {
                $scope.messagesToShow = $filter('filter')(privateMessages, { to: id }, true);
            }
            //$scope.$applasy();
            //console.log($scope.usersInPrivateChat);
        }
        $scope.closePrivateChat = function (u) {
            console.log(u);
            $scope.usersInPrivateChat.splice($scope.usersInPrivateChat.indexOf(u), 1);
            if ($scope.usersInPrivateChat.length != 0) {
                var last = $scope.usersInPrivateChat[$scope.usersInPrivateChat.length - 1]
                $scope.messagesToShow = $filter('filter')($scope.privateMessages, { to: last.id }, true);
                $scope.to = { id: last.id, type: 'private' };
            }
            else {
                if ($scope.roomsLoggedIn.length != 0) {
                    var last = $scope.roomsLoggedIn[$scope.roomsLoggedIn.length - 1]
                    $scope.messagesToShow = $filter('filter')($scope.roomMessages, { to: last.id }, true);
                    $scope.to = { id: last.id, type: 'room' };
                }
                else { $scope.messagesToShow = []; }
            }

        }

        $scope.showRoomList = function () {
            $(".selectList").toggleClass("on");
            $scope.ShowRoom = true;
        }
        $scope.showUserList = function () {
            $(".selectList").toggleClass("on");
            $scope.ShowRoom = false;
        }

        $scope.sendMessage = function () {
            if ($scope.to != null) {
                if ($scope.to.type == 'private') {
                    $scope.privateMessages.push(formatMessage($scope.to, $scope.me, $scope.message, true));
                    signalR.SendMessage($scope.message, $scope.to.id, true)
                }
                else {
                    $scope.roomMessages.push(formatMessage($scope.to, $scope.me, $scope.message, true));
                    signalR.SendMessage($scope.message, $scope.to.id, false)
                }
                if (!$scope.$$phase) {
                    $scope.$apply();
                }
            }
        }  
    })
    //});
    function formatMessage(to, from, message, status) {
        d = new Date();
        //d.toLocaleString();       // -> "2/1/2013 7:37:08 AM"
        //d.toLocaleDateString();   // -> "2/1/2013"
        //d.toLocaleTimeString();
        return {
            to: to.id,
            from: from.name,
            avatar: from.avatar,
            message: message,
            type: to.type,
            unread: status,
            time: d.toLocaleTimeString()
        }
    }

    function formatUser(users) {
        if (!users || users.length === 0) {
            return null;
        }
        return users.map(function (user) {
            return CreateUser(user);
        });   
    }
    function CreateUser(user) {
        if (user.Avatar == '' || user.Avatar == 'normal')
            user.Avatar = 'avatar4.jpg';
        return {
            name: user.Name,
            avatar: user.Avatar,
            id: user.Id,
            connectionId: user.ConnectionId,
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
                welcomeMessage: room.WelcomeMessage,
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
            status: true,
            sub: room.Name.substring(0, 1),
            users: users,
            unreadmessage: 0
        };

    }

    function getAvator(avatarFor, name, avatar) {
        if (avatar != '')
            return avatar;
        var char = room.name.substring(0, 1);
        if (avatarFor == 'room')
            return '';
        else
            return '';
    }

