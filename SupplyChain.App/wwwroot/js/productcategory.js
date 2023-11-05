var prcartegory = (() => {

    const OpenGeneralModal = () => {
        $('#general-partial-modal').find('.modal-title').text('Add Category');
        $('#general-modal-content').empty();
        $('#general-modal-content').load('/Setup/AddEditCategory');
    }

    const AddProductCategory = (event) => {
        event.preventDefault();
        //const hideloader = app.showloader('category-card');
        // Get the form element and create FormData object
        var formElement = event.target.closest('form');
        var formData = new FormData(formElement);
        let url = $(formElement).attr('action');
        app.SubmitForm(url, formData)
            .then((response) => {
                app.showhideModal('general-partial-modal');
                app.refreshElement('category-card-body', 'Setup', 'GetCategoriesCardData')
                app.SuccessAlertMessage(response.message);
                //hideloader();
            })
            .catch((xhr, status, error) => {
                if (error != undefined) {
                    app.FailAlertMessage(error.responseJSON.message);
                    app.reEnterFormData(formElement, formData);
                    //hideloader();
                } else {
                    console.error(xhr)
                    app.FailAlertMessage("Oops, Error Occurred, Please Try Again!", xhr);
                    //hideloader();
                }
            })

    }

    const OpenGeneralModalForEdit = (categoryId) =>  {
        let url = "/Setup/AddEditCategory"
        let data = { id: categoryId };
        app.ajax_request(url, 'GET', 'html', data)
            .then((resonse) => {
                $('#general-partial-modal').find('.modal-title').text('Eidt Category');
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