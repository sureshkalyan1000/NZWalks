using api8.Data;
using api8.Models;
using api8.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace api8.Repository
{
    public class SqlRegionRepository : IRegionsRepository
    {
        private readonly Api8DbContext Dbcontext;

        public SqlRegionRepository(Api8DbContext dbcontext)
        {
            Dbcontext = dbcontext;
        }

        public async Task<Regions> create(Regions Regions)
        {
            await this.Dbcontext.AddAsync(Regions);
            await Dbcontext.SaveChangesAsync();
            return Regions;
        }

        public async Task<Regions> delete(Guid id)
        {
            
            var domainmodel = await this.Dbcontext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (domainmodel == null)
            {
                return null;
            }
            this.Dbcontext.Remove(domainmodel);
            await Dbcontext.SaveChangesAsync();
            
            return domainmodel;

        }

        public async Task<List<Regions>> GetAllasync()
        {
            return await this.Dbcontext.Regions.ToListAsync();
        }

        public async Task<Regions> Getbyidasync(Guid id)
        {
            return await this.Dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Regions?> putbyid(Guid id, AddRegionDTO Regions)
        {
            var exregion = await Dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (exregion == null)
            {
                return null;
            }
            //DTO into Domainmodel
            exregion.Code = Regions.Code;
            exregion.Name = Regions.Name;
            exregion.RegionImageUrl = Regions.RegionImageUrl;

            Dbcontext.SaveChanges();
            return exregion;
        }
    }
}
