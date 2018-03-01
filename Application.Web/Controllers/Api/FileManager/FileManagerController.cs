using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/FileManager")]
    public class FileManagerController : Controller
    {
        public FileManagerController()
        {
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFiles([FromForm]ICollection<IFormFile> fileList)
        {
            var files = Request.Form.Files;

            try
            {
                foreach (var file in files)
                {
                    //_fileProcessor.CreateFile(file);
                }
                return Ok("File Uploaded.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
