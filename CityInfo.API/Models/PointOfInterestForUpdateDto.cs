using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {

        [Required(ErrorMessage = "You should provide a name value.")] // aplied Attribute
        [MaxLength(50)] // MAxleangth of Name is 50
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]// MAxleangth of Description is 200

        public string? Description { get; set; }
    }
}
