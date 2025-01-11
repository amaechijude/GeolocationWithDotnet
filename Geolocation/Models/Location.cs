using System.ComponentModel.DataAnnotations;

namespace Geolocation.Models
{
    public class LocationForms
    {
        [Required]
        [Url]
        public string? TargetLocationURL { get; set; }
        public double UserLatitude {get; set; }
        public double UserLongitude {get; set ; }
    }

    public class DistanceKm
    {
        public string? Message {get; set;}
        public string? Distance {get; set;}
    }
}
