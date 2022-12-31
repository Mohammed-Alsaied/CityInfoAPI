using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        //IEnumerable<City> GetCities();//synchronous Method
        //IQueryable<City> GetCities();

        //Task<IEnumerable<City>> GetCityAsync();

        //Writing async code on the server is all about freeing up threads as 
        //soon as p ible so they can be used for other tasks.
        //This improves the scalability of your application, and this is important to know. Writing async code is for scalability improvements,
        // know. Writing async code is for scalability improvements,not for performance improvements per se, although it often has   

        //asynchronously, one request can be handled by different threads, 
        //and one thread can handle different requests. 


        Task<IEnumerable<City>> GetCitiesAsync();
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<bool> CityExistsAsync(int cityId);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId,
            int pointOfInterestId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        Task<bool> CityNameMatchesCityId(string? cityName, int cityId);
        Task<bool> SaveChangesAsync();





    }
}
