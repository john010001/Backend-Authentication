using AutoMapper;
using Test.Authentication.Application.DTOs;
using Test.Authentication.Application.Features.Login.Queries.IniciarSesion;
using Test.Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Authentication.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<IniciarSesionQuery, AuthenticationRequest>().ReverseMap();
        }

        
    }
}
