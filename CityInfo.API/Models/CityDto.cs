namespace CityInfo.API.Models
{
    public class CityDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int NumberOfPointOfInterest
        {
            get
            {
                return pointsOfInterest.Count;
            }
        }

        public ICollection<PointOfInterestDto> pointsOfInterest { get; set; }
        = new List<PointOfInterestDto>();

    }
}
