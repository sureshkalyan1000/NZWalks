using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace api8.Models
{
    public class Image1
    {
        public Guid Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtention { get; set; }
        public long FileSizeInBytes {  get; set; }
        public string FilePath { get; set; }

    }
}
