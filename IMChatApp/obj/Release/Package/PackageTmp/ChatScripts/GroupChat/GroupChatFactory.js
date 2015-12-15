(function () {
    'use strict'
    //if (app != undefined) {
    appApp.factory("signalR", function ($rootScope) {
        var $hub = $.connection.srchat;
        var connection = null;
        var signalR = {
            startHub: function () {
                console.log("started");
                connection = $.connection.hub.start();
            },
            //////////////////// SERVER METHODS/////////////////
            Login: function (username) {
                connection.done(function () {
                    $hub.server.login(username);
                });
            },
            JoinChat: function () {
                connection.done(function () {
                    $hub.server.joinChat();
                });
            },
            JoinRoom: function (id) {
                connection.done(function () {
                    $hub.server.joinRoom(id);
                });
            },
            LeaveRoom: function (id) {
                connection.done(function () {
                    $hub.server.leaveRoom(id);
                });
            },
            ////////////////////// CLIENT METHODS////////////////////            
            RemoveNewUser: function (callback) {
                $hub.client.removeNewUser = callback;
            },
            
            GetRooms: function (callback) {
                    $hub.client.getrooms = callback;
                },
            RetRoomUsers: function (callback) {
                $hub.client.getRoomUsers = callback;
            },
            UserLoggedOut: function (callback) {
                $hub.client.userLoggedOut = callback;
            },
            AddNewUser: function (callback) {
                $hub.client.addNewUser = callback;
            },
            GetOnlineUsers: function (callback) {
                $hub.client.getOnlineUsers = callback;
            },
            NewOnlineUser: function (callback) {
                $hub.client.newOnlineUser = callback;
            },
            /// 
            NewOfflineUser: function (callback) {
                $hub.client.newOfflineUser = callback;
            },
            StatusChanged: function (callback) {
                $hub.client.statusChanged = callback;
            },
            IsTyping: function (callback) {
                $hub.client.isTyping = callback;
            },
            UpdateConnectionId: function (callback) {
                $hub.client.updateConnectionId = callback;
            }


        }
        return signalR;
    });
    //}
})();