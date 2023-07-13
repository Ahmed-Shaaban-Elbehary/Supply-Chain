var base_url = "/Setup"
var prcartegory = (() => {
    function OpenGeneralModal() {
        debugger;
        $('#general-modal-content').load('/Setup/AddEditCategory');
    }
    function AddProductCategory(event) {
        event.preventDefault();
        const hideloader = app.showloader('category-card');
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
    function DeleteSelectedItem(categoryId) {
        app.DeleteConfirmMessage().then((result) => {
            const hideloader = app.showloader('page-content');
            if (result.isConfirmed) {
                let url = base_url + "/DeleteCategory/" + categoryId;
                app.ajax_request(url, 'DELETE', 'json', null)
                    .then((resonse) => {
                        if (resonse.success == true) {
                            app.SuccessAlertMessage('Delete Category Item Process Compeleted Successfully!')
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
    function OpenGeneralModalForEdit(categoryId) {
        let url = "/Setup/AddEditCategory"
        let data = { id: categoryId };
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
        add_category: (event) => {
            AddProductCategory(event);
        },
        show_modal_init: () => {
            var button = document.getElementById('open-productCategory-modal');
            if (button != null)
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