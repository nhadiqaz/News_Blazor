using Generator;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        #region Dependency

        public IWebHostEnvironment WebHostEnvironment { get; }
        public ILogger<FilesController> Logger { get; }

        public FilesController(IWebHostEnvironment webHostEnvironment,ILogger<FilesController> logger)
        {
            WebHostEnvironment = webHostEnvironment;
            Logger = logger;
        }

        #endregion \Dependency

        #region Endpoints

        #region UploadImage

        [HttpPost]
        public async Task<string> UploadImage(List<IFormFile> files)
        {
            try
            {
                var _file = files[0];

                var _fileName = NameGenerator.GeneratorUniqCode();
                var _fileNameWithExtiontion = $"{_fileName}{Path.GetExtension(_file.FileName)}";
                var _path = Path.Combine(WebHostEnvironment.WebRootPath, "Images/Post", _fileNameWithExtiontion);

                using (var _stream = new FileStream(_path, FileMode.Create))
                {
                    await _file.CopyToAsync(_stream);
                }

                return _fileName;
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \UploadImage

        #region DeleteImage

        [HttpDelete("{imageName}")]
        public async Task<ActionResult> DeleteImage(string imageName)
        {
            try
            {
                var _path = Path.Combine(WebHostEnvironment.WebRootPath, $"Images/Post/{imageName}.jpg");

                if (System.IO.File.Exists(_path))
                {
                    System.IO.File.Delete(_path);
                }

                return Ok("Delete image is successfully");
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
            
        }

        #endregion \DeleteImage

        #endregion \Endpoints
    }
}
