"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();

connection.on("sendToUser", (EventObject) => {
    $("#notificationbadge").addClass('dot');
    var message = `<h2> ${EventObject.title} </h2>
                   <p>  ${EventObject.description} </p>
                   <p>  ${EventObject.publishedin} </p>`;

    app.notification(message);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});