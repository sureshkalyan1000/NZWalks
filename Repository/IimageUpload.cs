using api8.Models;

namespace api8.Repository
{
    public interface IimageUpload
    {
        public Task<Image1> ImageUpload(Image1 img);
    }
}
