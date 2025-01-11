// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const form = document.getElementById("form");

form.addEventListener("submit", async (event) => {
    event.preventDefault();

    const formData = new FormData(form);

    const response = await fetch("/api/location/", {
        method: 'POST',
        body: formData
    });

    if (response.status == 200) {
        const data = await response.json();
        console.log(data);
    } else {
        console.log("Error");
    }
});