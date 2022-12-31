using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        #region Aplling Validate Data [Required] Attribute for each fields
        //The data annotations are defined in System.ComponentModel.DataAnnotations, 
        [Required(ErrorMessage = "You should provide a name value.")] // aplied Attribute
        #endregion
        //Now, there are systems where the consumer is responsible for choosing the ID, 
        //So the rule is, use separate DTO for creating, updating, and returning resources. 
        [MaxLength(50)] // MAxleangth of Name is 50
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]// MAxleangth of Description is 200
        [Required]
        public string? Description { get; set; }
    }
}

