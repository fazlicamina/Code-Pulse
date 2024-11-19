using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);
            if (ModelState.IsValid)
            {
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now
                };
                blogImage=await imageRepository.Upload(file, blogImage);
                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    DateCreated = blogImage.DateCreated,
                    Title = blogImage.Title,
                    Url = blogImage.Url,
                    FileName = blogImage.FileName,
                    FileExtension = blogImage.FileExtension
                };

                return Ok(response);    
            }

            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] {".jpg", ".jpeg", ".png"};
            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower())){
                ModelState.AddModelError("file", "Unsupported format");
            }
            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "preveliko");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images=await imageRepository.GetAll();  
            var response= new List<BlogImageDto>();
            foreach (var image in images)
            {
                response.Add(new BlogImageDto
                {
                    Id=image.Id,
                    DateCreated = image.DateCreated,    
                    Title = image.Title,    
                    Url = image.Url,    
                    FileName = image.FileName,  
                    FileExtension = image.FileExtension 
                });
            }
            return Ok(response);
        }

    }
}
