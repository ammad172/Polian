// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    // Array of image URLs
    const images = [
        '/images/a.jpg',
        '/images/b.jpg',
        '/images/c.jpg',
        '/images/d.jpg',
    ];

    // Set initial background image
    let currentImageIndex = 0;
    $('body').css('background-image', `url(${images[currentImageIndex]})`);

    // Function to change background image every 7 seconds
    function changeBackground() {
        currentImageIndex = (currentImageIndex + 1) % images.length;
        $('body').css('background-image', `url(${images[currentImageIndex]})`);
    }

    // Set interval to change background every 7 seconds
    setInterval(changeBackground, 5000);
});

function DisplayBusyIndication() {
    $('.overlay').addClass('show');
}

function HideBusyIndication() {
    $('.overlay').addClass('hide');
}

$(window).on('beforeunload', function () {
    DisplayBusyIndication();
});

$(document).on('submit', 'form', function () {

    DisplayBusyIndication();
});

window.setTimeout(function () {
    HideBusyIndication();
}, 2000);



