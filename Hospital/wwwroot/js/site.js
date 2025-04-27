// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const input = document.getElementById("myDateInput");

input.addEventListener("input", function () {
    const date = new Date(this.value);
    const day = date.getUTCDay(); // 0 = Sunday, 5 = Friday, 6 = Saturday

    if (day === 5 || day === 6) {
        alert("Fridays and Saturdays are not allowed.");
        this.value = ""; // Clear the invalid date
    }
});