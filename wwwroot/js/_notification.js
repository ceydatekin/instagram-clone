"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

 


connection.on("GetNotificationForLike", function (user, message) {
    toastr.warning(user + " " + message, "Like");
    toastr.options.progressBar = true;
});

connection.on("GetNotificationForFollow", function (user, message) {
    toastr.warning(user + " " + message, "Like");
    toastr.options.progressBar = true;
});

connection.on("GetNotificationForShare", function (user, message) {
    console.log('paylaşım')
    alert(user + message);
});

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});
 