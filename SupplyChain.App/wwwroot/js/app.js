﻿/*******************************
 ***** GENERAL JAVASCRIPT ******
 *******************************/

// Attach an event handler to the entire document to listen for the start of AJAX requests.
$(document).ajaxStart(() => {
    // This function is executed when an AJAX request starts.
    $(document).ajaxSend(function (event, jqXHR, settings) {
        // Access the URL of the AJAX request
        const url = settings.url;
        // Use lastIndexOf to find the last '/'
        const lastSlashIndex = url.lastIndexOf('/');
        // Use substring to get the part of the URL after the last '/'
        const actionName = url.substring(lastSlashIndex + 1);

        if (actionName !== 'GetEventsList') {
            // You can place code here to show your loader or perform other setup tasks.
            // In this case, it shows the loader element with $('#loader').show().
            $('#loader').show();
        }
    });
});

// Attach an event handler to the entire document to listen for the end of AJAX requests.
$(document).ajaxStop(() => {
    // This function is executed when an AJAX request stops (completes).

    // Use setTimeout to add a brief delay (0.5 seconds) before hiding the loader.
    setTimeout(() => {
        // Inside this setTimeout function, you can add code to hide the loader.
        // In this case, it hides the loader element with $('#loader').hide().
        $('#loader').hide();
    }, 0); // The delay is in milliseconds, so 0.5000 seconds is equivalent to 500 milliseconds.

    // You can add additional actions or logic here as needed.
});

$(() => {
    // Write your JavaScript code.
    var inputs = document.querySelectorAll("input[type='number']");
    inputs.forEach(function (input) {
        input.addEventListener("keydown", function (event) {
            if (event.key === "e") {
                event.preventDefault();
            }
        });
    });

    // Get the current page or section name
    var currentPage = $('#currentPage').val();

    // Set the active nav-item
    $('li[data-nav="' + currentPage + '"]').addClass('active');

    //Call Events On Page Load.
    notification.get();

    $('.collapse').collapse()

    ////setInterval(() => {
    ////    app.checkCookieIfExist()
    ////}, 60000); //check cookie each 1 min.
    
});

/*******************************
 ***** APP MODULE ******
 *******************************/

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
 * ajax request provide promise, used in crud operation client side.
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
 * Subnmit from data server side.
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
            //location.reload();
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
app.FailAlertMessage = (message) => {
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
            //location.reload();
        }
    })

    return Toast.fire({
        icon: 'error',
        title: `error`,
        text: `${message}`
        
    })
}

/**
 * Show Real Time toaster
 * @param {string} title
 * @param {string} content
 * @param {date} date
 * @returns toast
 */
app.toaster = (title, content) => {
    $.toast({
        heading: title,
        text: content,
        position: 'bottom-right',
        showHideTransition: 'slide',
        icon: 'info',
        loader: false
    })
}

/**
 * Show hide target modalm by pass modal id.
 * @param {string} targetModal
 */
app.showhideModal = (targetModal) => {
    let isIn = $(`#${targetModal}`).hasClass('show');
    if (isIn) {
        $(`#${targetModal}`).modal('hide');
    } else {
        $(`#${targetModal}`).modal('show');
    }
}

/**
 * mehtod specified to refreshing an element from server side, using Ajax, to avoid view postback.
 * @param {string} targetElement
 * @param {string} controller
 * @param {string} action
 */
app.refreshElement = (targetElement, controller, action) => {
    let url = `/${controller}/${action}`;
    app.ajax_request(url, 'GET', 'html', null)
        .then((resonse) => {
            $(`#${targetElement}`).empty();
            $(`#${targetElement}`).html(resonse);
        })
        .catch((xhr, status, error) => {
            console.error(error);
        })

    //$(`#${targetElement}`).load(`/${controller}/${action}`);
}

/**
 * method specified to get cookies by name.
 * @param {stirng} cookieName
 * @returns
 */
app.getCookie = (cookieName) => {
    var dc = document.cookie;
    var prefix = cookieName + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1) {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    }
    else {
        begin += 2;
        var end = document.cookie.indexOf(";", begin);
        if (end == -1) {
            end = dc.length;
        }
    }
    // because unescape has been deprecated, replaced with decodeURI
    //return unescape(dc.substring(begin + prefix.length, end));
    return decodeURI(dc.substring(begin + prefix.length, end));
}

/**
 * Check cookie if it exist or not if not will redirect to a specific path.
 * @param {string} cookieName
 * @param {string} controller
 * @param {string} action
 */
app.checkCookieIfExist = (cookieName, controller, action) => {
    var myCookie = getCookie(`${cookieName}`); //check if browser has the specific cookie name 
    if (myCookie == null) {
        window.location.href = `/${controller}/${action}`;
    }
}