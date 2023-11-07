
const products = (() => {

    const RequestAdditionProductQuantities = () => {
        let productId = $("#ProductId").val();
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

    const CreateRequest = (e) => {
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
                    let message = `You have a new request for a ${response.data.messageTitle}, from ${response.data.sender}, 
                               which he wants an additional quantity ${response.data.messageBody}`;
                    SignalRModule.send_message(response.data.receiver.toString(), message);
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

    const AcceptRequest = (requestId) => {
        let url = "/Product/AcceptRequest";
        let data = { id: requestId };
        app.ajax_request(url, 'GET', 'json', data)
            .then((response) => {
                if (response.success) {
                    app.refreshElement('request-card-body', 'Product', 'GetRequestProductCardData');
                    app.SuccessAlertMessage(response.message);
                } else {
                    app.FailAlertMessage(response.message);
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

    const IgnoreRequest = (requestId) => {
        let url = "/Product/IgnoreRequest";
        let data = { id: requestId };
        app.ajax_request(url, 'GET', 'json', data)
            .then((response) => {
                if (response.success) {
                    app.refreshElement('request-card-body', 'Product', 'GetRequestProductCardData');
                    app.SuccessAlertMessage(response.message);
                } else {
                    app.FailAlertMessage(response.message);
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
            CreateRequest(e);
        },
        accept_request: (requestId) => {
            AcceptRequest(requestId);
        },
        ignore_request: (requestId) => {
            IgnoreRequest(requestId);
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