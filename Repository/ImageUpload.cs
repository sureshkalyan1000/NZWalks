using api8.Data;
using api8.Models;
using Microsoft.EntityFrameworkCore;

namespace api8.Repository
{
    public class ImageUpload : IimageUpload
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly Api8DbContext dbContext;

        public ImageUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, Api8DbContext DbContext) 
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = DbContext;
        }
        async Task<Image1> IimageUpload.ImageUpload(Image1 img)
        {
            var localpath = Path.Combine(webHostEnvironment.ContentRootPath,"Images", $"{img.FileName}{img.FileExtention}");
            var stream = new FileStream(localpath, FileMode.Create);

            await img.File.CopyToAsync(stream);

            //Push to DB
            img.FilePath=$"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.PathBase}/Images/{img.FileName}{img.FileExtention}";

            await this.dbContext.Images.AddAsync(img);
            await this.dbContext.SaveChangesAsync();

            return img;
        }
    }
}
