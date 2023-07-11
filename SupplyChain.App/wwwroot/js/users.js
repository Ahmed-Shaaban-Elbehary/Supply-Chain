var base_url = "/Setup";
var users = (() => {
    function OpenGeneralModal() {
        $('#general-modal-content').load('/Setup/AddEditUser');
    }
    function AddUser(event) {
        event.preventDefault();
        const hideloader = app.showloader('user-card');
        // Get the form element and create FormData object
        var formElement = event.target.closest('form');
        var formData = new FormData(formElement);
        let url = $(formElement).attr('action');
        app.SubmitForm(url, formData)
            .then((response) => {
                $('#general-partial-modal').modal('hide');
                app.SuccessAlertMessage(response.message);
                setTimeout(() => { hideloader(); location.reload(); }, 2000);
            })
            .catch((xhr, status, error) => {
                hideloader();
                app.FailAlertMessage(error);
            })

    }
    function DeleteSelectedItem(userId) {
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
                setTimeout(() => { hideloader(); }, 2000);
            }

        })
    }
    function OpenGeneralModalForEdit(userId) {
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
    return {
        add_user: (event) => {
            AddUser(event);
        },
        show_modal_init: () => {
            var button = document.getElementById('open-form-modal');
            if (button != null)
                button.addEventListener('click', OpenGeneralModal);
        },
        load_edit_modal: (id) => {
            OpenGeneralModalForEdit(id);
        },
        delete_user_item: (id) => {
            DeleteSelectedItem(id);
        }
    };
})();

//self-invoking
(function () {
    users.show_modal_init();
})();