using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Geolocation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private static async Task<string?> GetFullUrl(string shortUrl)
        {
            using HttpClient client = new();
            HttpRequestMessage request = new(HttpMethod.Head, shortUrl);
            HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if ((int)response.StatusCode >= 300 && (int)response.StatusCode < 310)
            {
                if (response.Headers.Location != null)
                {
                    return response.Headers.Location.ToString();
                }
            }
            return null;
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

        [HttpPost("get")]
        public async Task<IActionResult> GetDistance(string firstUrl, string secondUrl)
        {
            var firstFullUrl = await GetFullUrl(firstUrl);
            var secondFullUrl = await GetFullUrl(secondUrl);
            if (string.IsNullOrWhiteSpace(firstFullUrl) || string.IsNullOrWhiteSpace(secondFullUrl))
                return BadRequest("Error with url");

            var firstLongLat = GetCoordinates(firstFullUrl);
            var secondLongLat = GetCoordinates(secondFullUrl);

            if (firstLongLat.Count == 0 || secondLongLat.Count == 0)
                return BadRequest("Error getting Coordinates");

            double firstlong = firstLongLat[0];
            double firstlat = firstLongLat[1];
            double secondlong = secondLongLat[0];
            double secondlat = secondLongLat[1];

            var distance = Haversine(firstlat, firstlong, secondlat, secondlong);

            return Ok($"Your distance is {distance}  kilometers");
        }
    }
}