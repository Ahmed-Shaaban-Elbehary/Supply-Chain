const signalRModule = (() => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/NotificationHub")
        .build();

    connection.on("ReceiveNotification", (message) => {
        console.log(message);
        app.toaster(EventObject.title, EventObject.body);
    });

    connection.start().then(() => {
        // Connection is successfully established.
        // console.info("SignalR connection is established.");
        // You can now safely invoke methods.
    }).catch((err) => {
        // Connection failed.
        console.error("SignalR connection failed:", err);
    });

    return {
        sendNotificationToAll: (message) => {
            if (connection.state === signalR.HubConnectionState.Connected) {
                connection.invoke("SendNotificationToAll", message).catch((err) => console.error(err));
            } else {
                console.error("SignalR connection is not in a connected state. Unable to send the notification.");
            }
        },
        sendNotificationToUser: (userId, message) => {
            if (connection.state === signalR.HubConnectionState.Connected) {
                connection.invoke("SendNotificationToUser", userId, message).catch((err) => console.error(err));
            } else {
                console.error("SignalR connection is not in a connected state. Unable to send the notification.");
            }
        }
    };
})();


//"use strict";
//var all_users_connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();

//all_users_connection.on("sendToAllUsers", (EventObject) => {
//    notification.get();
//    notification.add_dot();
//    app.toaster(EventObject.title, EventObject.description, EventObject.publishedIn);
//});

//all_users_connection.start().catch(function (err) {
//    return console.error(err.toString());
//});