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
}
