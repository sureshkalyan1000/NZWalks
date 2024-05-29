using api8.Models;
using api8.Models.DTOs;
using AutoMapper;

namespace api8.mapping
{
    public class mapprofile : Profile
    {
        public mapprofile()
        {
            CreateMap<Regions, AddRegionDTO>().ReverseMap();
            CreateMap<Regions, RegionDTO>().ReverseMap();
            CreateMap<Walks, WalksDTO>().ReverseMap();
            CreateMap<Walks, AddwalkDTO>().ReverseMap();
        }
    }
}
