using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using ReadEase_C_.Interface;

namespace ReadEase_C_.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class PhotoController : ControllerBase
    {
        
        private readonly IConfiguration _configuration;
        private readonly IPhotoService _photoService;
        private readonly IUserManager _userManager;

        public PhotoController(IConfiguration configuration,IPhotoService photoService, IUserManager userManager)
        {
            _configuration = configuration;
            _photoService = photoService;
            _userManager = userManager;
        }

        [HttpPost("photo")]
        [Authorize]
        public IActionResult Photo(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file is uploaded.");

            // Check if the uploaded file is a valid image format
            string[] allowedImageTypes = { "image/jpeg", "image/png" }; // JPEG and PNG only
            if (!allowedImageTypes.Contains(file.ContentType))
                return BadRequest("Invalid file format. Only JPEG and PNG formats are allowed.");

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                // Resize the image while maintaining aspect ratio
                using var image = SixLabors.ImageSharp.Image.Load(memoryStream.ToArray());
                // Calculate resizing dimensions while maintaining aspect ratio
                int width = image.Width;
                int height = image.Height;
                int maxSize = 460;

                if (width > height)
                {
                    height = (int)Math.Round((double)height / width * maxSize);
                    width = maxSize;
                }
                else
                {
                    width = (int)Math.Round((double)width / height * maxSize);
                    height = maxSize;
                }

                // Resize the image
                image.Mutate(x => x.Resize(width, height));

                // Save the resized image with appropriate compression settings
                using var resizedMemoryStream = new MemoryStream();
                image.SaveAsJpeg(resizedMemoryStream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder
                {
                    Quality = 90 // Adjust quality as needed (0-100)
                });
                _photoService.InsertPhoto(_userManager.GetUserId(User), resizedMemoryStream.ToArray());
            }

            return Ok("Photo uploaded successfully.");
        }

        [HttpGet("getphoto")]
        [Authorize]
        public IActionResult GetPhoto()
        {

            int id = _userManager.GetUserId(User);
            byte[] photoBytes = _photoService.GetPhoto(id);

            if (photoBytes == null)
                return NotFound("No photo found for this user.");

            return File(photoBytes, "image/jpeg"); // adjust the content type based on your image format
        }



    }
}
