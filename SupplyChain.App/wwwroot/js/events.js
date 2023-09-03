var base_url = "/Event";
var events = (() => {

    OpenGeneralModal = () => {
        $('#general-modal-content').empty();
        $('#general-modal-content').load('/Event/AddEditEvent');
    }

    AddEvent = (event) => {
        event.preventDefault();
        const hideloader = app.showloader('event-card');
        var formElement = event.target.closest('form');
        var formData = new FormData(formElement);
        let url = $(formElement).attr('action');
        app.SubmitForm(url, formData)
            .then((response) => {
                if (!response.success) {
                    app.fillErrorMessageContainer(response.message);
                    app.reEnterFormData(formElement, formData);
                } else {
                    app.closeGeneralPatialModal();
                    app.SuccessAlertMessage(response.message);
                }
            })
            .catch((xhr, status, error) => {
                if (error != undefined) {
                    app.FailAlertMessage(error.responseJSON.message);
                    app.reEnterFormData(formElement, formData);
                    hideloader();
                } else {
                    app.FailAlertMessage("Oops, Error Occurred, Please Try Again!");
                    hideloader();
                }
            })
    }

    DeleteSelectedItem = (eventId) => {
        const hideloader = app.showloader('page-content');
        app.DeleteConfirmMessage().then((result) => {
            if (result.isConfirmed) {
                let url = base_url + "/DeleteUser/" + userId;
                app.ajax_request(url, 'DELETE', 'json', null)
                    .then((resonse) => {
                        if (resonse.success == true) {
                            app.SuccessAlertMessage('Delete User Compeleted Successfully!')
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
            } else {
                hideloader();
            }

        })
    }

    return {
        show_modal_init: () => {
            var button = document.getElementById('open-event-modal');
            if (button != null)
                button.addEventListener('click', OpenGeneralModal);
        }
    }
})();

//self-invoking
(function () {
    events.show_modal_init();
})();