﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplyingUniversityWeb.Models;
namespace ApplyingUniversityWeb.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users,Users>();
        }
    }
}
        