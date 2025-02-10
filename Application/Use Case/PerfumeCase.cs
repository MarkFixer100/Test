using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.IReposotory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Use_Case
{
    public class PerfumeCase
    {
        private readonly IReposotoryPerfume _perfumeRepository;
        private readonly IMapper _mapper;
        public PerfumeCase(IReposotoryPerfume perfumerepository , IMapper mapper) {
            
            _perfumeRepository = perfumerepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PerfumeDTO>> GetAllPerfumes()
        {
             var Perfumes = await _perfumeRepository.GetAll();

            return _mapper.Map<IEnumerable<PerfumeDTO>>(Perfumes);
        }
    }
}
