using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/v{version:apiVersion}/cities/{cityId}/pointsofinterest")]
    //assign city we want to make policy for it
    [Authorize(Policy = "MustBeFromAntwerp")]
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("2.0")]



    #region Before Inject ICityRepository & Auto Mapper
    //public class PointsOfInterestController : ControllerBase
    //{
    //    private readonly ILogger<PointsOfInterestController> _logger;
    //    //private readonly LocalMailService _mailService;
    //    private readonly IMailService _mailService;
    //    private readonly CitiesDataStore _citiesDataStore;

    //    public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
    //        //LocalMailService mailService)
    //        IMailService mailService,
    //        CitiesDataStore citiesDataStore)
    //    {
    //        //Constructor Injection
    //        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    //        _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
    //        _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));

    //        //another way
    //        //HttpContext.RequestServices.GetService(typeof(HttpContext)); 

    //    }









    //    //[HttpGet]
    //    //public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)//cityId = {cityId}
    //    //{
    //    //    var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);//Get City Id


    //    //    if (city == null)
    //    //    {
    //    //        return NotFound();
    //    //    }

    //    //    return Ok(city.pointsOfInterest);



    //    //}

    #region [HttpGet]


    //    [HttpGet("{ponitOfInterestId}", Name = "GetPointOfInterest")] //Id here == id passed in funct


    //    public ActionResult<PointOfInterestDto> GetPointOfInterest(
    //            int cityId, int ponitOfInterestId)//ponitsOfInterestId == [HttpGet("{ponitsOfInterestId}")]

    //    {

    //        try
    //        {
    //            //throw new Exception("Exception sample");


    //            var city = _citiesDataStore.Cities
    //            .FirstOrDefault(c => c.Id == cityId);
    //            if (city == null)
    //            {
    //                //_logger.Log;
    //                //_logger.LogDebug
    //                //_logger.LogError
    //                //_logger.LogTrace
    //                //_logger.LogCritical
    //                //_logger.LogWarning

    //                _logger.LogInformation(
    //                 $"City with id {cityId} wasn't found when accessing points of interest.");
    //                return NotFound();
    //            }


    //            //Find Point of interest

    //            var pointOfInterest = city.pointsOfInterest
    //                .FirstOrDefault(p => p.Id == ponitOfInterestId);
    //            if (pointOfInterest == null)
    //            {
    //                return NotFound();
    //            }

    //            return Ok(pointOfInterest);

    //        }


    //        catch (Exception ex)
    //        {

    //            _logger.LogCritical($"Exception while getting points of interest fot city id {cityId}.",
    //                ex);

    //            return StatusCode(500, "A problem happend while handling you request.");//at development only no production
    //        }
    //    }


    #endregion

    #region [HttpPost]
    //    [HttpPost]//A POST request returns the created resource in the response. 
    //              //This is a complex type, so as we're using the ApiController attribute,we do not need to add an additional [FromBody] annotation.If you want to do this, you can, but it's not necessary.   
    //    public ActionResult<PointOfInterestDto> CreatePointOfInterest(
    //        int cityId,
    //         //This is a complex type, so as we're using the ApiController attribute,we do not need to add an additional [FromBody] annotation.
    //         //If you want to do this, you can, but it's not necessary.
    //         //The content will be assumed to come from the request body.    
    //         //[FromBody] PointOfInterestFORCrationDto pointOfInterest) 
    //         PointOfInterestFORCrationDto poinstOfInterest)
    //    {
    //        #region ModelState
    //        //ModelState is a dictionary containing both the state of the model,
    //        //that's our PointOfInterestForCreationDto, It represents a collection of name value pairs that were submitted to our API,
    //        //It also contains a collection of error messages for each value submitted. 

    //        //if (!ModelState.IsValid)
    //        //{
    //        //    return BadRequest();
    //        //}
    //        //The nice thing is that this, too, is not necessary, 
    //        //again, thanks to the ApiController attribute.
    //        //Annotations are automatically checked during model binding and affect
    //        //ModelState dictionary, and the ApiController attribute ensures that in the 
    //        //case of an invalid ModelState, a 400 Bad Request is returned with the 
    //        //validation errors returned in the response body.So we can get rid of

    //        #endregion

    //        var city = _citiesDataStore.Cities
    //            .FirstOrDefault(c => c.Id == cityId);
    //        if (city == null)
    //        {
    //            return NotFound();
    //        }

    //        // demo - purposes - to be Improved
    //        var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(
    //            c => c.pointsOfInterest).Max(p => p.Id);

    //        // Mapping
    //        var finalPointOfInterest = new PointOfInterestDto()
    //        {
    //            Id = ++maxPointOfInterestId,
    //            Name = poinstOfInterest.Name,
    //            Description = poinstOfInterest.Description,

    //        };

    //        city.pointsOfInterest.Add(finalPointOfInterest);
    //        //  For POST, when all goes well, T the advised response is 201 Created. Helper methods 
    //        return CreatedAtRoute("GetPointOfInterest",//This one allows us to return a response with the location header.
    //                                                   //That location header will contain the URI where the newly created point of interest can be found.   

    //           //GetPointOfInterest route template needs a CityId and the ID of our PointOfInterest.

    //           new
    //           {
    //               CityId = cityId,
    //               ponitOfInterestId = finalPointOfInterest.Id
    //           }, finalPointOfInterest); //We also pass in the newly created point of interest, which will end up in the response body. And that should do it. 

    //        //return CreatedAtAction("GetPointOfInterest",

    //        //  new
    //        //  {
    //        //      CityId = cityId,
    //        //      ponitOfInterestId = finalPointOfInterest.Id
    //        //  }, finalPointOfInterest);
    //    }

    #endregion

    #region [HttpPut]
    //    [HttpPut("{pointOfInterestId}")]
    //    //As far as returning a value, we can choose. PUT actions can either return 
    //    //the updated resource as body of a 200 OK response, or nothing. That's a 204 NoContent result,
    //    //which means that the request completed
    //    //successfully, but there's nothing to return.
    //    //Both approaches are valid. In our case, there is something to be 


    //    #region Updating Resource,
    //    //There's two ways to update a resource,
    //    //a full update and a partial update
    //    #endregion

    //    #region Full Updating Resource


    //    public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
    //        PointOfInterestForUpdateDto pointOfInterest)
    //    {
    //        var city = _citiesDataStore.Cities
    //            .FirstOrDefault(c => c.Id == cityId);//Get City Id


    //        if (city == null)
    //        {
    //            return NotFound();
    //        }

    //        return Ok(city.pointsOfInterest);

    //        var pointOfInterestFromStore = city.pointsOfInterest

    //                        .FirstOrDefault(p => p.Id == pointOfInterestId);
    //        if (pointOfInterestFromStore == null)
    //        {
    //            return NotFound();
    //        }

    //        pointOfInterestFromStore.Name = pointOfInterest.Name;
    //        pointOfInterestFromStore.Description = pointOfInterest.Description;

    //        return NoContent();//Lastly we return a 204 NoContent. 
    //    }

    #endregion

    #region [HttpPatch]
    //    [HttpPatch]
    //    public ActionResult PartiallyUpdatePointOfInterest(
    //        int cityId, int pointOfInterestId,
    //        JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
    //    {
    //        var city = _citiesDataStore.Cities
    //            .FirstOrDefault(c => c.Id == cityId);//Get City Id


    //        if (city == null)
    //        {
    //            return NotFound();
    //        }



    //        var pointOfInterestFromStore = city.pointsOfInterest

    //                        .FirstOrDefault(p => p.Id == pointOfInterestId);
    //        if (pointOfInterestFromStore == null)
    //        {
    //            return NotFound();
    //        }

    //        var pointOfInterestToPatch =
    //            new PointOfInterestForUpdateDto()
    //            {
    //                Name = pointOfInterestFromStore.Name,
    //                Description = pointOfInterestFromStore.Description,
    //            };

    //        patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        if (!TryValidateModel(pointOfInterestToPatch))
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
    //        pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

    //        return NoContent();
    //    }

    #endregion

    #region [HttpDelete]
    //    [HttpDelete("{ponitOfInterestId}")]
    //    public ActionResult DeletePointOfInterest(int cityId, int ponitOfInterestId)

    //    {
    //        var city = _citiesDataStore.Cities
    //            .FirstOrDefault(c => c.Id == cityId);//Get City Id


    //        if (city == null)
    //        {
    //            return NotFound();
    //        }

    //        return Ok(city.pointsOfInterest);

    //        var pointOfInterestFromStore = city.pointsOfInterest

    //                        .FirstOrDefault(p => p.Id == ponitOfInterestId);
    //        if (pointOfInterestFromStore == null)
    //        {
    //            return NotFound();
    //        }

    //        city.pointsOfInterest.Remove(pointOfInterestFromStore);
    //        _mailService.Send("Pont of interest deleted",
    //            $"Pont of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id} was deleted");
    //        return NoContent();
    //    }

    #endregion
    //}

    #endregion

    #region After  Inject ICityRepository & Auto Mapper

    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        //private readonly LocalMailService _mailService;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        //private readonly CitiesDataStore? _citiesDataStore;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            //LocalMailService mailService)
            IMailService mailService,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {

            //Constructor Injection
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ??
                throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));


        }





        #region [HttpGet] GetPointsOfInterest(int cityId)

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(
           int cityId)
        {
            //selecting a city can access to our projct over token
            //var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

            //if (!await _cityInfoRepository.CityNameMatchesCityId(cityName, cityId))
            //{
            //    return Forbid();//user authanticatedd but dosnot have access
            //}



            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation(
                    $"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();
            }

            var pointsOfInterestForCity = await _cityInfoRepository
                .GetPointsOfInterestForCityAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
        }
        #endregion

        #region [HttpGet] GetPointOfInterest(int cityId, int ponitOfInterestId)
        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")] //Id here == id passed in funct
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(
            int cityId, int pointOfInterestId)//ponitsOfInterestId == [HttpGet("{ponitsOfInterestId}")]

        {

            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterest = await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
        }

        #endregion

        #region [HttpPost] CreatePointOfInterest
        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
           int cityId,
           PointOfInterestForCreationDto pointOfInterest)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(
                cityId, finalPointOfInterest);

            await _cityInfoRepository.SaveChangesAsync();

            var createdPointOfInterestToReturn =
                _mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                 new
                 {
                     cityId = cityId,
                     pointOfInterestId = createdPointOfInterestToReturn.Id
                 },
                 createdPointOfInterestToReturn);
        }


        #endregion

        #region [HttpPut] UpdatePointOfInterest

        #region Full Updating Resource

        [HttpPut("{pointOfInterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId,
                PointOfInterestForUpdateDto pointOfInterest)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, pointOfInterestEntity);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        #endregion
        #endregion

        #region [HttpPatch] PartiallyUpdatePointOfInterest
        [HttpPatch("{pointOfInterestId}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(
            int cityId, int pointOfInterestId,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDto>(
                pointOfInterestEntity);

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region [HttpDelete] DeletePointOfInterest
        [HttpDelete("{pointOfInterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(
            int cityId, int pointOfInterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

            _mailService.Send(
                "Point of interest deleted.",
                $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted.");

            return NoContent();
        }
        #endregion


    }
    #endregion
}



