"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();

connection.on("sendToUser", (EventObject) => {
    notification.GetEventsList();
    notification.add_dot();
    app.toaster(EventObject.title, EventObject.description, EventObject.publishedIn);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

