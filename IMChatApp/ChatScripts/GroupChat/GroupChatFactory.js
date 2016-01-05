(function () {
    'use strict'
    //if (app != undefined) {
    appApp.factory("signalR", function ($rootScope) {
        var $hub = $.connection.srchat;       
        var connection = null;
        var signalR = {
            startHub: function (token) {
                debugger;
                console.log("started");
                $.connection.hub.qs = { "token": token };
                connection = $.connection.hub.start();
            },
            //////////////////// SERVER METHODS/////////////////
            Login: function (username) {
                connection.done(function () {
                    $hub.server.login(username);
                });
            },
            JoinChat: function (name) {
                connection.done(function () {
                    $hub.server.joinChat(name);
                });
            },
            JoinRoom: function (id) {
                connection.done(function () {
                    $hub.server.joinRoom(id);
                });
            },
            JoinPrivateChat:function (id) {
                connection.done(function () {
                    $hub.server.joinPrivateChat(id);
                });
           },
            LeaveRoom: function (id) {
                connection.done(function () {
                    $hub.server.leaveRoom(id);
                });
            },
            SendMessage: function (message, id, ispvt) {
                connection.done(function () {
                    $hub.server.sendMessage(message,id,ispvt);
                });
            },
            ////////////////////// CLIENT METHODS////////////////////            
            RemoveNewUser: function (callback) {
                $hub.client.removeNewUser = callback;
            },
            
            GetRooms: function (callback) {
                    $hub.client.getrooms = callback;
                },
            GetRoomUsers: function (callback) {
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
            },
            //recivePrivateMessage(user,sender, message)
            RecivePrivateMessage: function (callback) {
                $hub.client.recivePrivateMessage = callback;
            },
            //reciveRoomMessage(id, sender, message);
            ReciveRoomMessage: function (callback) {
                $hub.client.reciveRoomMessage = callback;
            },
            UserLoggedOff: function (callback) {
                $hub.client.userLoggedOff = callback;
            }

            

        }
        return signalR;
    });
    //}
})();

$(function ($) {
    if (window.history && window.history.pushState) {
        $(window).on('popstate', function () {
            var hashLocation = location.hash;
            var hashSplit = hashLocation.split("#!/");
            var hashName = hashSplit[1];
            if (hashName !== '') {
                var hash = window.location.hash;
                if (hash === '') {
                    $.connection.srchat.connection.stop();
                }
            }
        });
        // window.history.pushState('forward', null, './#forward');
    }
});

window.onbeforeunload = function (e) {
    $.connection.srchat.connection.stop();
};