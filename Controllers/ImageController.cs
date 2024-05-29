using api8.Models;
using api8.Models.DTOs;
using api8.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IimageUpload iimageUpload;
        private readonly ILogger logger;

        public ImageController(IimageUpload iimageUpload, ILogger logger)
        {
            this.iimageUpload = iimageUpload;
            this.logger = logger;
        }
        [Route("Image")]
        [HttpPost]
        public async Task<IActionResult> Image(ImageDTO imageDTO)
        {
            ValidateFileUpload(imageDTO);
            if(ModelState.IsValid) 
            {
                try
                {
                    logger.LogInformation("Image upload function is invoked");
                    //DTO into Domain model
                    var img = new Image1
                    {
                        FileName = imageDTO.FileName,
                        File = imageDTO.File,
                        FileExtention = Path.GetExtension(imageDTO.File.FileName),
                        FileDescription = imageDTO.FileDescription,
                        FileSizeInBytes = imageDTO.File.Length

                    };
                    await iimageUpload.ImageUpload(img);

                    return Ok(img);
                }
                catch(Exception ex)
                {
                    logger.LogError(ex.Message, ex);
                    return BadRequest(ModelState);
                }

            }
            else
            {
                return BadRequest(ModelState);
            }

        }
        private void ValidateFileUpload(ImageDTO imageDTO)
        {
            var allowedEntentions = new List<string>() {".jpg", ".jpeg", ".png" };
            if(!allowedEntentions.Contains(Path.GetExtension(imageDTO.File.FileName)))
            {
                ModelState.AddModelError("File", "un supported file extention");
            }
            if (imageDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "un supported file extention");
            }
        }
    }
}
