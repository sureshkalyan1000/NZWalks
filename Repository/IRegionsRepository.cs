using api8.Models;
using api8.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace api8.Repository
{
    public interface IRegionsRepository
    {
        Task<List<Regions>> GetAllasync();
        Task<Regions?> Getbyidasync(Guid id);
        Task<Regions> create(Regions Regions);
        Task<Regions?> putbyid(Guid id, AddRegionDTO Regions);
        Task<Regions> delete(Guid id);
    }
}
