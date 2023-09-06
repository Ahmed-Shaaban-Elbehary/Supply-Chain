"use strict";

//build the hub connection
let connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build()

$(() => {
    connection.start().then(function () {
        alert("Connection to the hub success!");
    }).catch((error) => {
        console.error(error);
    })
})