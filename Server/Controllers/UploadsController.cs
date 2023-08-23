﻿using Generator;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public UploadsController(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

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
                await Console.Out.WriteLineAsync(ex.Message);
                throw ex;
            }
        }
    }
}
