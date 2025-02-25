using AutoMapper;
using Application.Dto;
using Application.StudentDTOS;
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

            CreateMap<Perfume, CreatePerfumeDTO>().ReverseMap();

            CreateMap<Perfume , UpdatePerfumeDTO>().ReverseMap();

             CreateMap<Student, StudentDTO>();

            CreateMap<StudentDTO, Student>();

            CreateMap<Student, CreateStudentDTO>().ReverseMap();

            CreateMap<Student , UpdateStudentDTO>().ReverseMap();
            
        }
    }
}
