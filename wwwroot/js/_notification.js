"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

 

connection.on("ReceiveMessage", function (user, message) {
   
});
 

connection.on("GetNotificationForLike", function (user, message) {
    alert(user + message);
});

connection.start().then(function () {
   
}).catch(function (err) {
    return console.error(err.toString());
});
 