// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const messageInputElem = document.getElementById('messageInput');
const charCountElem = document.getElementById('counter');

messageInputElem.addEventListener('input', function () {
    const maxLength = this.maxLength;
    const currentLength = this.value.length;
    const remainingChars = maxLength - currentLength;
    charCountElem.textContent = remainingChars + '/'+maxLength;
});