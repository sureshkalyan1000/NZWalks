using api8.Data;
using api8.Models;
using api8.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace api8.Repository
{
    public class WalksRepository : IWalks
    {
        private readonly Api8DbContext dbcontext;

        public WalksRepository(Api8DbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<Walks> CreateWalksAsync(Walks Walkss)
        {
            //Walkss.Id = Guid.NewGuid();
            await this.dbcontext.Walks.AddAsync(Walkss);
            await this.dbcontext.SaveChangesAsync();
            return Walkss;
        }

        public async Task<Walks?> Delete(Guid id)
        {
            var eDomain = await this.dbcontext.Walks.FirstOrDefaultAsync(x => x.Id == id); 
            if (eDomain == null) { return null; }
            dbcontext.Walks.Remove(eDomain);
            await this.dbcontext.SaveChangesAsync();
            return eDomain;

        }

        public async Task<List<Walks>> GetAllWalks(string? filterOn, string? value, string? orderBy, bool ascending = true,
             int pagenum = 1, int size = 1000)
        {
            var walk = dbcontext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            
            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(value) == false) 
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk = walk.Where(x => x.Name.Contains(value));
                }
            }

            //Sorting
            if(string.IsNullOrWhiteSpace(orderBy) == false)
            {
                if(orderBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walk = ascending ? walk.OrderBy(x => x.LengthInKm) : walk.OrderByDescending(x => x.LengthInKm);
                }
            }

            //paginate
            var skipResult = (pagenum -1) *size;

            return await walk.Skip(skipResult).Take(size).ToListAsync();
        }

        public async Task<Walks?> GetByID(Guid id)
        {
            return await this.dbcontext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walks> putById(Guid id, Walks walks)
        {
            var domain = await this.dbcontext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (domain == null) { return null; }
            domain.Name = walks.Name;
            domain.Description = walks.Description;
            domain.LengthInKm = walks.LengthInKm;
            domain.WalkImageUrl = walks.WalkImageUrl;
            domain.Region = walks.Region;
            domain.Difficulty = walks.Difficulty;

            await this.dbcontext.SaveChangesAsync();
            return walks;
        }
    }
}
