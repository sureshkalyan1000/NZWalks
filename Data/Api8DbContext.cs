using api8.Models;
using Microsoft.EntityFrameworkCore;

namespace api8.Data
{
    public class Api8DbContext:DbContext
    {
        public Api8DbContext(DbContextOptions<Api8DbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }
        public  DbSet<Difficulty> Difficulty { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<Walks> Walks { get; set; }
        public DbSet<Image1> Images { get; set; }

    }
}
