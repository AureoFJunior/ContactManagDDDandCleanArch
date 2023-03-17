using AutoMapper;
using ContactManag.Domain.Models;
using ContactManag.Domain.DTOs;

namespace ContactManag.Web.Config
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingsConfigs = new MapperConfiguration(config =>
            {
                config.CreateMap<Contact, ContactDTO>().ReverseMap();
                config.CreateMap<Person, PersonDTO>().ReverseMap();
                config.CreateMap<User, UserDTO>().ReverseMap();
            });

            return mappingsConfigs;
        }
    }
}
