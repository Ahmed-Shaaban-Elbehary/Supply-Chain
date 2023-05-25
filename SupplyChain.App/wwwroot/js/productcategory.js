let base_url = "/Setup"
var prcartegory = (() => {
    function OpenGeneralModal() {
        $('#general-modal-content').load('/Setup/AddEditCategory');
    }
    function AddProductCategory(event) {
        event.preventDefault();
        // Get the form element and create FormData object
        var formElement = event.target.closest('form');
        var formData = new FormData(formElement);
        if (formData.checkValidity()) {
            const hideloader = app.showloader('general-partial-modal');
            let url = $(formElement).attr('asp-action');
            app.SubmitForm(url, formData)
                .then((response) => {
                    app.SuccessAlertMessage(response)
                })
                .catch((xhr, status, error) => {
                    console.error(error);
                })
        } else {

        }
    }
    function DeleteSelectedItem(categoryId) {
        app.DeleteConfirmMessage().then((result) => {
            const hideloader = app.showloader('page-content');
            if (result.isConfirmed) {
                let data = JSON.stringify({ id: categoryId })
                let url = "/Setup/DeleteCategory";
                app.ajax_request(url, 'DELETE', 'json', data)
                    .then((resonse) => {
                        if (resonse.success == true) {
                            app.SuccessAlertMessage('Delete Category Item Process Compeleted Successfully!')
                                .then((result) => {
                                    if (result.dismiss === Swal.DismissReason.timer) {
                                        setTimeout(() => { hideloader(); }, 2000);
                                        // Reload the page after the Toast is closed
                                        location.reload();
                                    }
                                });
                        } else {
                            app.FailAlertMessage('Fail To Delete Item, Please Try Again!');
                        }
                    })
                    .catch((xhr, status, error) => {
                        app.FailAlertMessage(error);
                    })
            }

        })
    }
    function OpenGeneralModalForEdit(categoryId) {
        let url = "/Setup/AddEditCategory"
        let data = { id: categoryId };
        app.ajax_request(url, 'GET', 'html', data)
            .then((resonse) => {
                console.log(resonse);
                $('#general-partial-modal').find('#general-modal-content').html(resonse);
                $('#general-partial-modal').modal('show');
            })
            .catch((xhr, status, error) => {
                console.error(error);
            })
    }

    return {
        add_category: (event) => {
            AddProductCategory(event);
        },
        show_modal_init: () => {
            var button = document.getElementById('open-form-modal');
            button.addEventListener('click', OpenGeneralModal);
        },
        load_edit_modal: (id) => {
            OpenGeneralModalForEdit(id);
        },
        delete_category_item: (id) => {
            DeleteSelectedItem(id);
        }
    };
})();

//self-invoking
(function () {
    prcartegory.show_modal_init();
})();