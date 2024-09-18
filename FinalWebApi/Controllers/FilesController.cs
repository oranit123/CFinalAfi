using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly string _uploadFolderPath;

        public FilesController(IWebHostEnvironment env)
        {
            _uploadFolderPath = Path.Combine(env.ContentRootPath, "Uploads");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                Directory.CreateDirectory(_uploadFolderPath);

                var originalFileName = Path.GetFileNameWithoutExtension(model.File.FileName);
                var fileExtension = Path.GetExtension(model.File.FileName);

                DeleteExistingFiles(originalFileName);

                var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                var fileName = $"{originalFileName}_{timeStamp}{fileExtension}";
                var filePath = Path.Combine(_uploadFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var fileUrl = Path.Combine(baseUrl, "Uploads", fileName);

                return Ok(new { FilePath = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
            }
        }

        private void DeleteExistingFiles(string originalFileName)
        {
            var files = Directory.GetFiles(_uploadFolderPath, $"{originalFileName}_*");

            foreach (var file in files)
            {
                System.IO.File.Delete(file);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(7);
        }
    }

    public class FileUploadModel
    {
        public IFormFile File { get; set; }
    }
}
