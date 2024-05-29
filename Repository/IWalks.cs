using api8.Models;
using api8.Models.DTOs;
using System.Drawing;

namespace api8.Repository
{
    public interface IWalks
    {
        Task<Walks> CreateWalksAsync(Walks Walkss);
        Task<List<Walks>> GetAllWalks(string? filterOn, string? value, string? orderBy, bool ascending, int pagenum=1,int size = 1000);
        Task<Walks?> GetByID(Guid id);
        Task<Walks> putById(Guid id, Walks walks);
        Task<Walks?> Delete(Guid id);
    }
}
