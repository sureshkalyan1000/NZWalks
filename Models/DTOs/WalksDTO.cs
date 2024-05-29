using System.ComponentModel.DataAnnotations;

namespace api8.Models.DTOs
{
    public class WalksDTO
    {
        // public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }

        //navigation properties
        public Regions Region { get; set; }
        public Difficulty Difficulty { get; set; }


    }
}
