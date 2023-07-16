var base_url = "/Setup";
var users = (() => {

    OpenGeneralModal = () => {
        $('#general-modal-content').empty();
        $('#general-modal-content').load('/Setup/AddEditUser');
    }

    AddUser = (event) => {
        event.preventDefault();
        const hideloader = app.showloader('user-card');
        var formElement = event.target.closest('form');
        var formData = new FormData(formElement);
        let url = $(formElement).attr('action');
        app.SubmitForm(url, formData)
            .then((response) => {
                if (!response.success) {
                    app.fillErrorMessageContainer(response.message);
                    app.reEnterFormData(formElement, formData);
                    hideloader();
                } else {
                    app.closeGeneralPatialModal();
                    app.SuccessAlertMessage(response.message);
                    hideloader();
                }
            })
            .catch((xhr, status, error) => {
                if (error.responseJSON) {
                    app.FailAlertMessage(error.responseJSON.message);
                    app.reEnterFormData(formElement, formData);
                    hideloader();
                } else {
                    app.FailAlertMessage("Oops, Error Occurred, Please Try Again!");
                    hideloader();
                }
            })
    }

    DeleteSelectedItem = (userId) => {
        app.DeleteConfirmMessage().then((result) => {
            const hideloader = app.showloader('page-content');
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
            }

        })
    }

    OpenGeneralModalForEdit = (userId) => {
        let url = "/Setup/AddEditUser"
        let data = { id: userId };
        app.ajax_request(url, 'GET', 'html', data)
            .then((resonse) => {
                $('#general-partial-modal').find('#general-modal-content').html(resonse);
                $('#general-partial-modal').modal('show');
            })
            .catch((xhr, status, error) => {
                console.error(error);
            })
    }

    PasswordChanged = () => {
        $('#IsPasswordChanged').val(true);
        console.log($('#IsPasswordChanged').val())
    }

    return {
        add_user: (event) => {
            AddUser(event);
        },
        show_modal_init: () => {
            var button = document.getElementById('open-user-modal');
            if (button != null)
                button.addEventListener('click', OpenGeneralModal);
        },
        load_edit_modal: (id) => {
            OpenGeneralModalForEdit(id);
        },
        delete_user_item: (id) => {
            DeleteSelectedItem(id);
        },
        passwordChanged: () => {
            PasswordChanged();
        }
    };
})();

//self-invoking
(function () {
    users.show_modal_init();
})();