"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();

connection.on("sendToUser", (EventObject) => {
    $("#notificationbadge").addClass('dot')
    console.log(EventObject);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});