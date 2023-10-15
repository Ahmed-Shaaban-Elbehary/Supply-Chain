var base_url = "/Product";

const products = (() => {

    const RequestAdditionProductQuantities = () => {
        let productId = $("#productId").find().val();
        let url = base_url + "/RequestAdditionProductQuantities";
        let data = { id: productId };
        app.ajax_request(url, 'GET', 'html', data)
            .then((resonse) => {
                $('#general-partial-modal').find('#general-modal-content').html(resonse);
                app.showhideModal('general-partial-modal');
                //$('#general-partial-modal').modal('show');
            })
            .catch((xhr, status, error) => {
                console.error(error);
            })
    }

    return {
        request_additional_product_quantities: () => {
            RequestAdditionProductQuantities();
        }
    }
});



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



