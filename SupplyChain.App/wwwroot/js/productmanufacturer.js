var base_url = "/Setup"
var prmanufacturer = (() => {
    function OpenGeneralModal() {
        $('#general-modal-content').empty();
        $('#general-modal-content').load('/Setup/AddEditManufacturer');
    }
    function AddProductManufacturer(event) {
        event.preventDefault();
        const hideloader = app.showloader('manufacturer-card');
        // Get the form element and create FormData object
        var formElement = event.target.closest('form');
        var formData = new FormData(formElement);
        let url = $(formElement).attr('action');
        app.SubmitForm(url, formData)
            .then((response) => {
                app.closeGeneralPatialModal();
                app.SuccessAlertMessage(response.message);
                hideloader();
            })
            .catch((xhr, status, error) => {
                hideloader();
                app.FailAlertMessage(error);
            })

    }
    function DeleteSelectedItem(manufacturerId) {
        app.DeleteConfirmMessage().then((result) => {
            const hideloader = app.showloader('page-content');
            if (result.isConfirmed) {
                let url = base_url + "/DeleteManufacturer/" + manufacturerId;
                app.ajax_request(url, 'DELETE', 'json', null)
                    .then((resonse) => {
                        if (resonse.success == true) {
                            app.SuccessAlertMessage('Delete manufacturer Item Process Compeleted Successfully!')
                                .then((result) => {
                                    if (result.dismiss === Swal.DismissReason.timer) {
                                        location.reload();
                                    }
                                });
                        } else {
                            hideloader();
                            app.FailAlertMessage('Fail To Delete Item, Please Try Again!');
                        }
                    })
                    .catch((xhr, status, error) => {
                        hideloader();
                        app.FailAlertMessage(error);
                    });
                hideloader();
            }

        })
    }
    function OpenGeneralModalForEdit(manufacturerId) {
        let url = "/Setup/AddEditmanufacturer"
        let data = { id: manufacturerId };
        app.ajax_request(url, 'GET', 'html', data)
            .then((resonse) => {
                $('#general-partial-modal').find('#general-modal-content').html(resonse);
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