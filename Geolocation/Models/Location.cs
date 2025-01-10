using System.ComponentModel.DataAnnotations;

namespace Geolocation.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Required]
        public string? LocationName { get; set; }
        [Url]
        public string? GoogleMapURL { get; set; }
    }
}
