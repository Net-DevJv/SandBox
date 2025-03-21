using ApiUsers.DTO;
using ApiUsers.Models;
using AutoMapper;

namespace ApiUsers.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<User, ListUserDTO>();
        }
    }
}
