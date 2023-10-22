
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



