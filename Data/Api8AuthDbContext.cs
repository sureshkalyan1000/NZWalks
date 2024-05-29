using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace api8.Data
{
    public class Api8AuthDbContext:IdentityDbContext
    {
        public Api8AuthDbContext(DbContextOptions<Api8AuthDbContext> options) :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var ReaderId = "75c8326b-0f55-462e-b20a-a45a7890993f";
            var WriterId = "f28f9e28-910a-44b2-b19d-1d75e38c023c";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = ReaderId,
                    ConcurrencyStamp = ReaderId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = WriterId,
                    ConcurrencyStamp = WriterId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
