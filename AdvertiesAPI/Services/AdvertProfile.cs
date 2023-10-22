using AdvertiesAPI.Models;
using AutoMapper;

namespace AdvertiesAPI.Services
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<AdvertDbModel, AdvertiesModel>().ReverseMap();
        }
    }
}
