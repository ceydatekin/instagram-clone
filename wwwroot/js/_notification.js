"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

 


connection.on("GetNotificationForLike", function (user, message) {
    toastr.warning(user + " " + message, "LIKE");
    toastr.options.progressBar = true;
});

connection.on("GetNotificationForFollow", function (user, message) {
    toastr.warning(user + " " + message, "FOLLOW");
    toastr.options.progressBar = true;
});

connection.on("GetNotificationForShare", function (user, message) {
    console.log('paylaşım')
    toastr.warning(user + " " + message, "SHARE");
    toastr.options.progressBar = true;
});

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});
 