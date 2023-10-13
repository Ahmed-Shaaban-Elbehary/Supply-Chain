var base_url = "/Event";

const events = (() => {

    const OpenGeneralModal = () => {
        $('#general-modal-content').empty();
        $('#general-modal-content').load('/Event/AddEditEvent');
    }

    const AddEvent = (event) => {
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
                    app.showhideModal('general-partial-modal');
                    //app.SuccessAlertMessage(response.message);
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

    const DeleteSelectedItem = (eventId) => {
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
                                        $('#calendar').fullCalendar('refetchEvents');
                                        //location.reload();
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

    const OpenGeneralModalForEdit = (eventId) => {
        let url = base_url + "/AddEditEvent"
        let data = { id: eventId };
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

    const GetEventById = (eventId) => {
        let url = base_url + "/GetEventById/" + eventId;
        app.ajax_request(url, 'GET', 'json', null)
            .then((response) => {
                console.log(response);
            })
            .catch((xhr, status, error) => {
                console.error(error);
            });

    }

    const OnEventBlockQuoteClick = (eventId) => {
        let url = `/Event/UpdateEventAsRead/${eventId}`;
        app.ajax_request(url, 'GET', 'html', null)
            .then((res) => {
                $('#event-details-partial-modal').find('#event-details-modal-content').html(res);
                $('#event-details-partial-modal').modal('show');
            })
            .catch((xhr, status, error) => {
                console.error(error);
            })
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
        },
        on_event_block_quote_click: (eventId) => {
            OnEventBlockQuoteClick(eventId);
        }
    }
})();

//self-invoking
(() => {
    events.show_modal_init();
})();