using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Geolocation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Geolocation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private static async Task<string> GetFullUrl(string shortUrl)
        {
            {
                using HttpClient client = new();
                // Allow auto-redirect
                HttpResponseMessage response = await client.GetAsync(shortUrl);
                return response.RequestMessage?.RequestUri?.ToString() ?? string.Empty;
            }
        }

        private static List<double> GetCoordinates(string fullUrl)
        {
            string pattern = @"@(-?\d+\.\d+),(-?\d+\.\d+)";
            Match match = Regex.Match(fullUrl, pattern);

            if (match.Success)
            {
                // Extract longitude and latitude as doubles
                double longitude = double.Parse(match.Groups[1].Value);
                double latitude = double.Parse(match.Groups[2].Value);

                return [longitude, latitude];
            }
            return [];
        }
        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
        private static double Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Radius of the Earth in kilometers

            // Convert degrees to radians
            double lat1Rad = DegreesToRadians(lat1);
            double lon1Rad = DegreesToRadians(lon1);
            double lat2Rad = DegreesToRadians(lat2);
            double lon2Rad = DegreesToRadians(lon2);

            // Differences
            double dLat = lat2Rad - lat1Rad;
            double dLon = lon2Rad - lon1Rad;

            // Haversine formula
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Distance in kilometers
            return R * c;
        }

        [HttpPost]
        public async Task<IActionResult> GetDistance([FromForm] LocationForms locationForms)
        {
            var targetUrl = locationForms.TargetLocationURL;
            if (string.IsNullOrWhiteSpace(targetUrl))
                return BadRequest("Cannot urls cannot be empty");

            var targetFullUrl = await GetFullUrl(targetUrl);

            if (string.IsNullOrWhiteSpace(targetFullUrl))
                return BadRequest("Error fetching long url");

            // var firstLongLat = GetCoordinates(firstFullUrl);
            var targetLongLat = GetCoordinates(targetFullUrl);

            if (targetLongLat.Count == 0)
                return BadRequest("Error getting target coordinates Coordinates");

            double userlong = (double)locationForms.UserLongitude;
            double userlat = (double)locationForms.UserLatitude;
            double targetlong = targetLongLat[0];
            double targetlat = targetLongLat[1];

            var distance = Haversine(userlat, userlong, targetlat, targetlong);

            return Ok(new DistanceKm
            {
                Message = "Successful",
                Distance = $"{distance:F2}  Kilometers"
            });
        }
    }
}