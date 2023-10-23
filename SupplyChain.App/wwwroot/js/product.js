
const products = (() => {

    const RequestAdditionProductQuantities = () => {
        let productId = $("#productId").val();
        let url = "/Product/RequestAdditionProductQuantities";
        let data = { id: productId };
        app.ajax_request(url, 'GET', 'html', data)
            .then((resonse) => {
                $('#general-partial-modal').find('.modal-title').text('Request Additional Quantity');
                $('#general-partial-modal').find('#general-modal-content').html(resonse);
                app.showhideModal('general-partial-modal');
            })
            .catch((xhr, status, error) => {
                if (error != undefined) {
                    app.FailAlertMessage(error.responseJSON.message);
                    //hideloader();
                } else {
                    console.error(xhr)
                    app.FailAlertMessage("Oops, Error Occurred, Please Try Again!", xhr);
                    //hideloader();
                }
            })
    }

    const CreateProductQuantityRequest = (e) => {
        e.preventDefault();
        //const hideloader = app.showloader('user-card');
        var formElement = e.target.closest('form');
        var formData = new FormData(formElement);
        let url = $(formElement).attr('action');
        app.SubmitForm(url, formData)
            .then((response) => {
                if (!response.success) {
                    app.fillErrorMessageContainer(response.message);
                    app.reEnterFormData(formElement, formData);
                } else {
                    app.showhideModal('general-partial-modal');
                    //hideloader();
                }
            })
            .catch((xhr, status, error) => {
                if (error != undefined) {
                    app.FailAlertMessage(error.responseJSON.message);
                    app.reEnterFormData(formElement, formData);
                    //hideloader();
                } else {
                    console.error(xhr)
                    app.FailAlertMessage("Oops, Error Occurred, Please Try Again!", xhr);
                    //hideloader();
                }
            })
    }

    return {
        request_additional_product_quantities: () => {
            RequestAdditionProductQuantities();
        },
        create_product_quantity_request: (e) => {
            CreateProductQuantityRequest(e);
        }
    }
})();


//self-invoking
(() => {
    var fileInput = document.getElementById('image-input');
    if (fileInput != null) {
        var fileLabel = fileInput.nextElementSibling;

        fileInput.addEventListener('change', (event) => {
            const fileName = event.target.value.split('\\').pop();
            if (fileName != "")
                fileLabel.innerHTML = fileName;
            else
                fileLabel.innerHTML = "Choose Image";
        });
    }


    var events_count = $("#eventsCount").val();
    if (events_count == "True") {
        setTimeout(() => {
            $("#eventsModal").modal('show');
        }, 1000);
    };

})();

var notificationModule = (() => {
    //global connection for all module.
    let connection;

    const get_connection = () => {
        connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub").build();
    }

    const get_sender_user_id = () => {
        return "SenderUserID";
    }

    const get_receiver_user_id = () => {
        return "ReceiverUserID";
    }

    const start_connection = () => {
        connection.start().catch((err) => console.error(err));
    }

    const push_notification = () => {
        if (!connection) {
            get_connection();
        }

        connection.on("sendToUser", (articleHeading, articleContent) => {
            // Rest of your code
        });

        start_connection();
    }
    
    return {
        GetConnection: () => {
            return get_connection();
        },
        GetSenderUserId: () => {
            return get_sender_user_id();
        },
        GetReceiverUserId: () => {
            return get_receiver_user_id();
        },
        PushNotification: () => {
            push_notification();
        }
    }

})();








