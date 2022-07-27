using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationMicroservice.Models.DTO;
using AuthorizationMicroservice.Models;

namespace AuthorizationMicroservice.AutoMapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
        
    }
}
