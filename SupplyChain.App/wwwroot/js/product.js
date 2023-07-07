/******************************
******* EVENT LISTENERS *******
*******************************/

const fileInput = document.getElementById('image-input');
const fileLabel = fileInput.nextElementSibling;

fileInput.addEventListener('change', (event) => {
    const fileName = event.target.value.split('\\').pop();
    if (fileName != "")
        fileLabel.innerHTML = fileName;
    else
        fileLabel.innerHTML = "Choose Image";
});