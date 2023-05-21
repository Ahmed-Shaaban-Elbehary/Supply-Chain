var app = app || {}

/**
 * pass html element id, to show loader on it.
 * @param {string} targetId
 */
app.showloader = (targetId) => {
    // Get the target element by ID
    var target = document.getElementById(targetId);

    // Set the position style of the target element to 'relative'
    target.style.position = 'relative';

    // Disable pointer events for the target element and its child elements
    target.style.pointerEvents = 'none';

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

    // Return a function to hide the spinner and re-enable pointer events
    return () => {
        spinnerContainer.style.display = 'none';
        target.style.pointerEvents = 'auto';
    };
};
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
 * @returns toast
 */
app.SuccessAlertMessage = (msg) => {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 1500,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })

    return Toast.fire({
        icon: 'success',
        title: '' + msg + ''
    })
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
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })

    return Toast.fire({
        icon: 'error',
        title: 'error: ' + msg + '',
    })
}
