using CruiseVoyage.DbContext;
using CruiseVoyage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CruiseVoyage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {
        private readonly CruiseDbContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PhotoController(CruiseDbContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile image, [FromHeader] int idCustomer)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Создаем папку для изображений (если не существует)
            var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filename = image.FileName.Replace(' ', '_');
            // Генерируем уникальное имя файла
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + filename;

            // Определяем путь для сохранения файла
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Сохраняем файл на сервере
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            // Сохраняем информацию о загруженном изображении в базу данных
            var photo = new Photo { PhotoName = uniqueFileName, UrlPath = filePath, idCustomer = idCustomer};
            await _dbContext.Photo.AddAsync(photo);
            await _dbContext.SaveChangesAsync();

            // Возвращаем путь к сохраненному файлу
            return Ok(new { filePath });
        }
    }
}