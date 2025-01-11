# GeolocationWithDotnet

**GeolocationWithDotnet** is a web application built using ASP.NET Core that calculates the distance between a user's current location and a specific destination provided as a Google Maps URL. The application leverages JavaScript's Geolocation API for obtaining the user's location and processes the distance calculation using the Haversine formula.

---

## Features

- Accepts Google Maps URLs as input.
- Utilizes the Geolocation API to retrieve the user's current location.
- Calculates the distance between the user and the destination using latitude and longitude values.
- User-friendly interface for entering the Google Maps URL and displaying results.
- Built with ASP.NET Core for backend processing and JavaScript for client-side geolocation.

---

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Installation](#installation)
3. [Usage](#usage)
4. [Technologies Used](#technologies-used)
---

## Prerequisites

Ensure you have the following installed on your system:

- [.NET Core SDK](https://dotnet.microsoft.com/download) (v9.0 or later)

- A modern web browser that supports the Geolocation API

---

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/amaechijude/GeolocationWithDotnet
   ```

2. Navigate to the project directory:
   ```bash
   cd GeolocationWithDotnet
   ```

3. Restore the dependencies:
   ```bash
   dotnet restore
   ```

4. Build the project:
   ```bash
   dotnet build
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

6. Open your browser and navigate to `https://localhost:****`.

---

## Usage

1. On the home page, paste a Google Maps URL into the input field.
2. Allow location access when prompted by your browser.
3. Click the "Calculate Distance" button.
4. View the calculated distance in kilometers displayed below the input field.

### Example Input

Google Maps URL:
```
https://www.google.com/maps/place/Eiffel+Tower/@48.8588443,2.2943506
```

### Output

```
Your current location is approximately 12.5 km away from the Eiffel Tower.
```

---

## Technologies Used

### Backend
- ASP.NET Core 9.0

### Frontend
- HTML5 and CSS3
- JavaScript (Geolocation API)

### Other
- Google Maps for destination URLs
- Haversine formula for distance calculation

---


