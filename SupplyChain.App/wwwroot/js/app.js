var app = app || {}

/**
 * pass html element id, to show loader on it.
 * @param {string} targetId
 */
app.showloader = (targetId) => {

    // Get the target element by ID
    var target = document.getElementById(targetId);

    // Create a new element to contain the spinner
    var spinnerContainer = document.createElement('div');
    spinnerContainer.classList.add('spinner-container');

    // Add the spinner to the container element
    var spinner = document.getElementById('spinner').cloneNode(true);
    spinner.removeAttribute('id');
    spinnerContainer.appendChild(spinner);

    // Add the container element to the target element
    target.appendChild(spinnerContainer);

    // Show the spinner
    spinnerContainer.style.display = 'flex';

    // Return a function to hide the spinner
    return () => {
        spinnerContainer.style.display = 'none';
    };
}
/**
 * 
 * @param {string} url
 * @param {string} type
 * @param {string} datatype
 * @param {object} data
 * @param {function()} successCallback
 * @param {function()} errorCallback
 */
app.ajax_request = (url, type, datatype, data) => {
    return new Promise(function (resolve, reject) {
        console.log(data);
        $.ajax({
            url: url,
            type: type,
            dataType: datatype,
            contentType: "application/json; charset=utf-8",
            data: data,
            success: function (response) {
                resolve(response);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                reject(errorThrown);
            }
        });
    });
}
/**
 * Specific Ajax Request using new method delete to specify and target only HTTPDelete JsonResult In the Controller.
 * Make sure to pass id within url.
 * @param {string} _url
 * @returns
 */
app.ajax_delete_request = (_url) => {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: _url,
            method: "DELETE",
            success: function (data) {
                resolve(data);
            },
            error: function (xhr, status, error) {
                reject(xhr, status, error);
            }
        });
    });
}
/**
 * Swal Callback Function, for alert successfull processes.
 * @param {string} msg
 * @returns alert
 */
app.SuccessAlertMessage = (msg) => {
    return Swal.fire(
        'Message',
        '' + msg + '',
        'success'
    )
}
/**
 * Swal Callback Function, for alert confirmation message before deleting action.
 * @returns alert
 */
app.DeleteConfirmMessage = () => {
    return Swal.fire({
        title: 'Are you sure?',
        text: 'You will not be able to recover this item!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!'
    })
}
/**
 * Swal Callback Function, for alert Fail Processes
 * @param {string} msg
 * @returns alert
 */
app.FailAlertMessage = (msg) => {
    return Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: '' + msg + '',
    })
}
