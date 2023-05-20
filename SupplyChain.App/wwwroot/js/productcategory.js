var prcartegory = (() => {

    function OpenGeneralModal() {
        $('#general-modal-content').load('/Setup/AddEditCategory');
    }

    function DeleteSelectedItem(categoryId) {
        
        let url = "/Setup/DeleteCategory/" + categoryId + "";
        app.DeleteConfirmMessage().then((result) => {
            if (result.isConfirmed) {
                app.ajax_delete_request(url)
                    .then((resonse) => {
                        if (resonse.success == true) {
                            app.SuccessAlertMessage('Delete Category Item Process Compeleted Successfully!')
                                .then((result) => {
                                if (result.dismiss === Swal.DismissReason.timer) {
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