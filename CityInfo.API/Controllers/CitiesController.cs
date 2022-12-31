using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfo.API.Controllers
{
    //Attrebutes
    [Route("api/v{version:apiVersion}/cities")]
    //template and url for this controller
    //[Route("api/controller")]//But to be honest, I'm not a big fan of this approach.If we were to have refactoring of our code later on and rename the Controller class,the URI to our cities resource would automatically change as well.
    ////This can be considered an advantage, but for APIs it's more of a disadvantage, in my opinion. As a resource,URI should remain the same, regardless of what the underlying class is named. That's of no importance to the consumer,   
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]




    #region Before Using Repository
    //public class CitiesController : ControllerBase
    //{
    //    private CitiesDataStore _citiesDataStore;

    //    public CitiesController(CitiesDataStore citiesDataStore)
    //    {
    //        _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
    //    }



    #region Without using Statue Code
    //ControllerBase contains basic functionality controllers need like access to the model state, the current user,and common methods for returning responses.  
    //You could also derive from Controller,but that Base class contains additional helper methods to use when returning views, which isn't needed when building an API.   
    //[HttpGet("api/cities")]
    //[HttpGet]
    //public JsonResult GetCities()
    //{
    //    //return new JsonResult(new List<Object>
    //    //{
    //    //    //new { id = 1, Name = "New York City"},
    //    //    //new { id = 2, Name = "Antwerp"},
    //    //    // Attribute Routing Error
    //    //    //Routing matches a request URI to an action on a controller. 



    //    //});

    //    //Return from CitiesDataStore
    //    //var temp = new JsonResult(CitiesDataStore.Current.Cities);
    //    //temp.StatusCode = 200;


    //    return new JsonResult(CitiesDataStore.Current.Cities);
    //}

    //[HttpGet("{id}")]
    //public JsonResult GetCity(int id)

    //{
    //    return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
    //}
    #endregion

    #region With using Statue Code
    //Use Statues Codes

    //[HttpGet]
    //public ActionResult<IEnumerable<CityDto>> GetCities()
    //{
    //    return Ok(_citiesDataStore.Cities);

    //    //no need NotFound() It just happens to be an empty list,and an empty list does not warrant a NotFound.  
    //}


    //[HttpGet("{id}")]
    //public ActionResult<CityDto> GetCity(int id)

    //{
    //    // find city
    //    var cityToReturn = _citiesDataStore.Cities
    //        .FirstOrDefault(c => c.Id == id);
    //    if (cityToReturn == null)
    //    {
    //        return NotFound();

    //    }

    //    return Ok(cityToReturn);//return it using an Ok helper method.This will return the city with a 200 OK status code,  
    #endregion




    //}
    #endregion




    #region After Using Repository And Inject ICityInfoRepository & IMapper 
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;

        public CitiesController(ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        //Use Statues Codes

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCitiesPageSize)
            {
                //pageNumber = maxCitiesPageSize;
                pageSize = maxCitiesPageSize;
            }

            var (cityEntities, paginationMetadata) = await _cityInfoRepository
                .GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            #region Manual Mapping
            ////Mapping
            //var result = new List<CityWithoutPointOfInterestDto>();
            //foreach (var cityEntitie in cityEntities)
            //{
            //    result.Add(new CityWithoutPointOfInterestDto
            //    {
            //        Id = cityEntitie.Id,
            //        Name = cityEntitie.Name,
            //        Description = cityEntitie.Description,
            //    });

            //}

            //return Ok(result);
            #endregion

            #region Auto Mapper
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));

            #endregion
        }

        /// <summary>
        /// Get a city by id
        /// </summary>
        /// <param name="id">The id of the city to get</param>
        /// <param name="includePointsOfInterest">Whether or not to include the points of interest</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested city</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCity(
            int id, bool includePointsOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }
        #endregion
    }
}
