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
                let url = base_url + "/DeleteEvent/" + eventId;
                app.ajax_request(url, 'DELETE', 'json', null)
                    .then((resonse) => {
                        if (resonse.success == true) {
                            app.SuccessAlertMessage('Delete Event Compeleted Successfully!')
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

    OpenGeneralModalForEdit = (eventId) => {
        let url = base_url + "/AddEditEvent"
        let data = { id: eventId };
        app.ajax_request(url, 'GET', 'html', data)
            .then((resonse) => {
                $('#general-partial-modal').find('#general-modal-content').html(resonse);
                $('#general-partial-modal').modal('show');
            })
            .catch((xhr, status, error) => {
                console.error(error);
            })
    }

    GetEventById = (eventId) => {
        let url = base_url + "/GetEventById/" + eventId;
        app.ajax_request(url, 'GET', 'json', null)
            .then((response) => {
                console.log(response);
            })
            .catch((xhr, status, error) => {
                console.error(error);
            });

    }

    return {
        show_modal_init: () => {
            var button = document.getElementById('open-event-modal');
            if (button != null)
                button.addEventListener('click', OpenGeneralModal);
        }, add_event: (event) => {
            AddEvent(event);
        }, load_edit_modal: (eventId) => {
            OpenGeneralModalForEdit(eventId);
        },
        get_event_by_id: (eventId) => {
            GetEventById(eventId);
        },
        delete_event_item: (eventId) => {
            DeleteSelectedItem(eventId);
        }
    }
})();

//self-invoking
(function () {
    events.show_modal_init();
})();