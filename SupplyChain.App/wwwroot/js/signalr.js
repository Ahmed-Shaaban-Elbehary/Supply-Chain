"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();

connection.on("sendToUser", (EventObject) => {
    $("#notificationbadge").addClass('dot');
    const publishedIn = app.getDateTimeFormat(EventObject.publishedIn);
    app.notification(EventObject.title, EventObject.description, publishedIn);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

