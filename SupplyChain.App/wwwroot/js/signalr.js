const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub") // The URL should match your hub's URL
    .build();

connection.on("ReceiveNotification", (message) => {
    // Handle the received notification here
    app.toaster(null, message);
    console.log("Received notification: " + message);
});

connection.start().catch((err) => console.error(err));


/*------------------------
 ---- SIGNAL R MODULE ----
 -------------------------*/

var SignalRModule = (() => {

    const sendNotification = (userId, message) => {
        connection.invoke("SendNotification", userId, message)
            .catch((err) => console.error(err));
    }
    return {
        send_message: (userId, message) => {
            sendNotification(userId, message);
        }
    }
})();
