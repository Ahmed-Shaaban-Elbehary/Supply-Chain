
const products = (() => {

    const RequestAdditionProductQuantities = () => {
        let productId = $("#productId").val();
        let url = "/Product/RequestAdditionProductQuantities";
        let data = { id: productId };
        app.ajax_request(url, 'GET', 'html', data)
            .then((resonse) => {
                $('#general-partial-modal').find('.modal-title').text('Request Additional Quantity');
                $('#general-partial-modal').find('.modal-dialog').addClass('modal-sm');
                $('#general-partial-modal').find('#general-modal-content').html(resonse);
                app.showhideModal('general-partial-modal');
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



