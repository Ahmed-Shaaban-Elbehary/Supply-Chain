
var prmanufacturer = (() => {

    const OpenGeneralModal = () => {
        $('#general-partial-modal').find('.modal-title').text('Add Manufacturer');
        $('#general-modal-content').empty();
        $('#general-modal-content').load('/Setup/AddEditManufacturer');
    }

    const AddProductManufacturer = (event) => {
        event.preventDefault();
        const hideloader = app.showloader('manufacturer-card');
        // Get the form element and create FormData object
        var formElement = event.target.closest('form');
        var formData = new FormData(formElement);
        let url = $(formElement).attr('action');
        app.SubmitForm(url, formData)
            .then((response) => {
                app.showhideModal('general-partial-modal');
                app.SuccessAlertMessage(response.message);
                app.refreshElement('manufacturer-card-body', 'Setup', 'GetManufacturerCardData')
                hideloader();
            })
            .catch((xhr, status, error) => {
                if (error != undefined) {
                    app.FailAlertMessage(error.responseJSON.message);
                    app.reEnterFormData(formElement, formData);
                    hideloader();
                } else {
                    console.error(xhr)
                    app.FailAlertMessage("Oops, Error Occurred, Please Try Again!", xhr);
                    hideloader();
                }
            })

    }

    const DeleteSelectedItem = (manufacturerId) => {
        const hideloader = app.showloader('manufacturer-card');
        app.DeleteConfirmMessage().then((result) => {
            if (result.isConfirmed) {
                let url = `/Setup/DeleteManufacturer/${manufacturerId}`;
                app.ajax_request(url, 'DELETE', 'json', null)
                    .then((response) => {
                        if (response.success == true) {
                            app.SuccessAlertMessage(`${response.message}`);
                            app.refreshElement('manufacturer-card-body', 'Setup', 'GetManufacturerCardData')
                            hideloader();
                        } else {
                            app.FailAlertMessage(`${response.message}`);
                            hideloader();
                        }
                    })
                    .catch((xhr, status, error) => {
                        if (error != undefined) {
                            app.FailAlertMessage(error.responseJSON.message);
                            app.reEnterFormData(formElement, formData);
                            hideloader();
                        } else {
                            console.error(xhr)
                            app.FailAlertMessage("Oops, Error Occurred, Please Try Again!", xhr);
                            hideloader();
                        }
                    });
                hideloader();
            } else {
                hideloader();
            }
        })
    }

    const OpenGeneralModalForEdit = (manufacturerId) => {
        let url = "/Setup/AddEditmanufacturer"
        let data = { id: manufacturerId };
        app.ajax_request(url, 'GET', 'html', data)
            .then((response) => {
                $('#general-partial-modal').find('.modal-title').text('Edit Manufacturer');
                $('#general-partial-modal').find('#general-modal-content').html(response);
                $('#general-partial-modal').modal('show');
            })
            .catch((xhr, status, error) => {
                console.error(error);
            })
    }

    return {
        add_manufacturer: (event) => {
            AddProductManufacturer(event);
        },
        show_modal_init: () => {
            var button = document.getElementById('open-productManufacturer-modal');
            if (button != null)
                button.addEventListener('click', OpenGeneralModal);
        },
        load_edit_modal: (id) => {
            OpenGeneralModalForEdit(id);
        },
        delete_manufacturer_item: (id) => {
            DeleteSelectedItem(id);
        }
    };
})();

//self-invoking
(function () {
    prmanufacturer.show_modal_init();
})();