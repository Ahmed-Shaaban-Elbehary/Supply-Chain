var app = app || {}

// Object to keep track of loader status for each element
var loaderStatus = {};

/**
 * Show loader on the specified element.
 * @param {string} targetId - The ID of the element to show the loader on.
 */
app.showloader = (targetId) => {
    var target = document.getElementById(targetId);
    if (!target) {
        return;
    }

    // Check if loader is already shown on this element
    if (loaderStatus[targetId]) {
        return;
    }
    loaderStatus[targetId] = true;

    // Set the position style of the target element to 'relative'
    target.style.position = 'relative';

    // Disable pointer events for the target element and its child elements
    target.style.pointerEvents = 'none';

    // Create a new element to contain the spinner
    var spinnerContainer = document.createElement('div');
    spinnerContainer.classList.add('spinner-container');

    // Add the spinner to the container element
    var spinner = document.getElementById('spinner').cloneNode(true);
    spinner.classList.remove("d-none");
    spinner.removeAttribute('id');
    spinnerContainer.appendChild(spinner);

    // Add the container element to the target element
    target.appendChild(spinnerContainer);

    // Show the spinner
    spinnerContainer.style.display = 'flex';

    // Return a function to hide the spinner and re-enable pointer events
    return function () {
        if (loaderStatus[targetId]) {
            loaderStatus[targetId] = false;
            spinnerContainer.style.display = 'none';
            target.style.pointerEvents = 'auto';
        }
    };
};

/**
 * 
 * @param {any} strDate
 * @returns
 */
app.getDateTimeFormat = (strDate) => {
    let date = new Date(strDate);

    const yyyy = date.getFullYear();
    let mm = date.getMonth() + 1; // Months start at 0!
    let dd = date.getDate();
    let h = date.getHours();
    let m = date.getMinutes();
    let s = date.getSeconds();

    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;
    if (h < 10) mm = '0' + h;
    if (s < 10) mm = '0' + s;

    let fullDate = `${dd}/${mm}/${yyyy} ${h}:${m}:${s}`;
    return fullDate;
}

/** */
app.closeGeneralPatialModal = () => {
    $('#general-partial-modal').modal('hide');
}

/**
 * 
 * @param {any} msg
 */
app.fillErrorMessageContainer = (msg) => {
    var linkElement = $('#error-message-content');
    linkElement.parent().removeClass('d-none');
    linkElement.html(msg);
}

/**
 * 
 * @param {string} url
 * @param {string} method
 * @param {string} datatype
 * @param {object} data
 * @param {function()} successCallback
 * @param {function()} errorCallback
 */
app.ajax_request = (url, method, datatype, data) => {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: url,
            method: method,
            dataType: datatype,
            contentType: "application/json; charset=utf-8",
            data: data,
            success: (response) => {
                resolve(response);
            },
            error: (jqXHR, textStatus, errorThrown) => {
                reject(errorThrown);
            }
        });
    });
}

/**
 * 
 * @param {any} url
 * @param {any} formData
 * @returns
 */
app.SubmitForm = (url, formData) => {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: url,
            data: formData,
            method: 'POST',
            processData: false, // Required for FormData
            contentType: false, // Required for FormData
            success: (response) => {
                resolve(response);
            },
            error: (jqXHR, textStatus, errorThrown) => {
                reject(errorThrown);
            }
        })
    })
}

/**
 * 
 * @param {any} formElement
 * @param {any} formData
 */
app.reEnterFormData = (formElement, formData) => {
    $(formElement).find('input[type!="checkbox"], select, textarea')
        .each(function () {
            $(this).val(formData.get($(this).attr('name')));
        });

    $(formElement).find('input[name="Password"]').val('');
    $(formElement).find('input[name="Submit"]').val('Save');
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
        },
        didClose: () => {
            location.reload();
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
        },
        didClose: () => {
            location.reload();
        }
    })

    return Toast.fire({
        icon: 'error',
        title: 'error: ' + msg + '',
    })
}

/**
 * Show Real Time Notification
 * @param {string} title
 * @param {string} content
 * @param {date} date
 * @returns toast
 */
app.notification = (title, content, date) => {
    toastr.info(`<div class="p-1">
                    <p class="d-block text-truncate mb-0" style="max-width: 250px;">
     		                ${content}
    	            </p>
                    <small class="d-block float-right">${date}</small>
                </div>`,
        `${title}`);

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}


app.GetEventsList = () => {
    let url = '/Event/GetEventsList';
    app.ajax_request(url, 'GET', 'json', null)
        .then((response) => {
            $.each(response, (index, val) => {
                let html = `<blocspinnerkquote id="event-blockqoute" onclick="events.on_event_block_quote_click(${val.id})" class="blockquote bg-cloudy">
                              <p class="mb-0 text-md text-muted">${val.description}</p>
                              <footer class="blockquote-footer">
                                ${app.getDateTimeFormat(val.publishedIn)} 
                                  <cite title="Source Title">
                                    <i class="fas fa-calendar-check mr-2"></i> ${val.title}
                                  </cite>
                              </footer>
                            </blockquote>`;

                $('#notification-list').find('#notification-container').append(html);
            });

        })
        .catch((jqXHR, textStatus, errorThrown) => {
            console.error(jqXHR);
            console.error(errorThrown);
        });
}
