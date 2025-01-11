using System.ComponentModel.DataAnnotations;

namespace Geolocation.Models
{
    public class LocationURLS
    {
        [Required]
        [Url]
        public string? UserLocationURL { get; set; }
        [Required]
        [Url]
        public string? TargetLocationURL { get; set; }
    }

    public class DistanceKm
    {
        public string? Message {get; set;}
        public string? Distance {get; set;}
    }
}
