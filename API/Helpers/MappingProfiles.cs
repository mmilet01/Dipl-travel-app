using AutoMapper;
using Core.DTOs;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Users, UserData>().ReverseMap();
            CreateMap<UserDto, UserData>().ReverseMap();
            CreateMap<Memories, MemoryDto>().ReverseMap();
            CreateMap<Users, UserDto>().ReverseMap();
            CreateMap<UsersTaggedOnMemory, UsersTaggedOnMemoryDto>().ReverseMap();
            CreateMap<Messages, MessagesDto>().ReverseMap();
          //  CreateMap<List<Messages>, List<MessagesDto>>().ReverseMap();

        }
    }
}
