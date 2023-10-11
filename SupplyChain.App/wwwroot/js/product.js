/******************************
******* EVENT LISTENERS *******
*******************************/

var fileInput = document.getElementById('image-input');
if (fileInput != null) {
    var fileLabel = fileInput.nextElementSibling;

    fileInput.addEventListener('change', (event) => {
        const fileName = event.target.value.split('\\').pop();
        if (fileName != "")
            fileLabel.innerHTML = fileName;
        else
            fileLabel.innerHTML = "Choose Image";
    });
}


var events_count = $("#eventsCount").val();
if (events_count == "True") {
    setTimeout(() => {
        $("#eventsModal").modal('show');
    }, 1000);
};

