using AutoMapper;
using Application.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class MappingProfile:Profile 
    {
        public MappingProfile() {

            CreateMap<Perfume, PerfumeDTO>();
            CreateMap<PerfumeDTO, Perfume>();
        }
    }
}
