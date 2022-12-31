using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [Authorize]
    [Produces("application/json")]

    [ApiController]
    public class FilesController : ControllerBase

    {
        #region Using FileExtensionContentTypeProvider to Manuplate Files

        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                ?? throw new System.ArgumentNullException(
                    nameof(fileExtensionContentTypeProvider));
        }

        #endregion

        [HttpGet("{fileId}")]
        public ActionResult GetFiles(string fileId)
        {
            //FileContentResult;
            //FileStreamResult    //accepts a stream to read from and the content type, and then you've got 
            //PhysicalFileResult and VirtualFileResult. These two allow you to pass through a file name and a content type. 

            //return File(); //use those, it's more convenient to call into return File.This method is defined on the ControllerBase, and it acts as a wrapper around the aforementioned FileResult subclasses.  

            //running from. First, let's look up the actual file, debending ont the fileId
            // demo code

            var pathToFile = "getting-started-with-rest-slides.pdf";

            // check whether the file exist
            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(
                pathToFile, out var contentType))
            {
                contentType = "application/octet-stream";//which is a default media type for arbitrary binary data. You can look at it as a catch all for those file types you don't have more specific information about.
                                                         //When we call into return File,we no longer pass through text/plain as content type, but we pass through the resulting contentType from our FileExtensionContentTypeProvider.   
            }
            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            //return File(bytes, "text/plain", Path.GetFileName(pathToFile));//Media Type For PDF apllication/pdf
            return File(bytes, contentType, Path.GetFileName(pathToFile));//Media Type For PDF apllication/pdf




        }
    }
}
