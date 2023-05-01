// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


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