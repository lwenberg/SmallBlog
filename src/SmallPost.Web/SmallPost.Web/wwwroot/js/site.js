// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.addEventListener('DOMContentLoaded', function () {

    const messageInputElem = document.getElementById('messageInput');
    const charCountElem = document.getElementById('counter');

    function updateCounter() {
        const maxLength = messageInputElem.maxLength;
        const currentLength = messageInputElem.value.length;
        const remainingChars = maxLength - currentLength;
        charCountElem.textContent = remainingChars + '/' + maxLength;
    }

    messageInputElem.addEventListener('input', updateCounter);

    updateCounter();

});