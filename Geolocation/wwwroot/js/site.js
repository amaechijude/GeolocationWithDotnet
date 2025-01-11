// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const form = document.getElementById("form");
form.addEventListener("submit", async (event) => {
    event.preventDefault();
    await CurrentLocation();
});

async function CurrentLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition)
    } else {
        alert("Could'nt get location. Try givin your browser acces")
    }
}

async function showPosition(position) {
    const userLatitude = position.coords.latitude;
    const userLongitude = position.coords.longitude;

    const bodyData = new FormData(form)
    bodyData.append("UserLat", userLatitude);
    bodyData.append("UserLon", userLongitude)
    // fetch to the backend
    const response = await fetch("/api/location/", {
        method: 'POST',
        body: bodyData
    });

    if (response.status == 200) {
        const data = await response.json();
        console.log(data);
    } else {
        console.log("Error");
    }
}
