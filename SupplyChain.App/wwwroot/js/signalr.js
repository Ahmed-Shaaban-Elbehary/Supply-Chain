"use strict";
var all_users_connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();

all_users_connection.on("sendToAllUsers", (EventObject) => {
    notification.get();
    notification.add_dot();
    app.toaster(EventObject.title, EventObject.description, EventObject.publishedIn);
});

all_users_connection.start().catch(function (err) {
    return console.error(err.toString());
});