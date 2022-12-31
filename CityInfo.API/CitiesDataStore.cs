

using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        //public static CitiesDataStore Current { get; } = new CitiesDataStore();//Singletpn Pattern

        public CitiesDataStore()
        {
            // init dummy data
            Cities = new List<CityDto>()
            {
                    new CityDto()
                    {
                        Id = 1,
                        Name = "New York City",
                        Description = "The one with that big park",

                        pointsOfInterest = new List<PointOfInterestDto>()
                        {
                            new PointOfInterestDto()
                            {
                                Id = 1,
                                Name = "Central Park",
                                Description = "The most visited urban park"
                            },

                             new PointOfInterestDto()
                            {
                                Id = 2,
                                Name = "Central Park",
                                Description = "The most visited urban park"
                            },
                        }
                    },

                    new CityDto()
                    {
                        Id = 2,
                        Name = "Antwerp",
                        Description = "The one that big park",

                        pointsOfInterest = new List<PointOfInterestDto>()
                            {
                                new PointOfInterestDto()
                                {
                                    Id = 3,
                                    Name = "Central Park",
                                    Description = "The most visited urban park"
                                },

                                 new PointOfInterestDto()
                                {
                                    Id = 4,
                                    Name = "Central Park",
                                    Description = "The most visited urban park"
                                },
                            }

                    },

                    new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with that big Tower",

                    pointsOfInterest = new List<PointOfInterestDto>()
                        {
                            new PointOfInterestDto()
                            {
                                Id = 5,
                                Name = "Central Park",
                                Description = "The most visited urban park"
                            },

                             new PointOfInterestDto()
                            {
                                Id = 6,
                                Name = "Central Park",
                                Description = "The most visited urban park"
                            },
                        }
                },
            };
        }
    }
}
