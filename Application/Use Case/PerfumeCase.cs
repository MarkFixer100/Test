using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.IReposotory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public PerfumeCase(IReposotoryPerfume perfumerepository, IMapper mapper)
        {

            _perfumeRepository = perfumerepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PerfumeDTO>> GetAllPerfumesAsync()
        {
            var Perfumes = await _perfumeRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<PerfumeDTO>>(Perfumes);
        }

        public async Task<PerfumeDTO> GetAsync(int id)
        {
            var Perfume = await _perfumeRepository.GetAsync(u => u.Id == id);

            return _mapper.Map<PerfumeDTO>(Perfume);
        }

        public async Task<PerfumeDTO> AddAsync(CreatePerfumeDTO model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            if (await _perfumeRepository.GetAsync(u => u.Name.ToLower() == model.Name.ToLower()) != null)
            {
                throw new ValidationException("Villa already exists");
            };

            Perfume perfume = _mapper.Map<Perfume>(model);

            await _perfumeRepository.CreateAsync(perfume);

            return _mapper.Map<PerfumeDTO>(perfume);
        }

        public async Task Delete(int id)
        {
            var Perfume = await _perfumeRepository.GetAsync(p => p.Id == id);

           await _perfumeRepository.Remove(Perfume);

            await _perfumeRepository.SaveAsync();
        }

        public async Task UpdateAsync(int id , UpdatePerfumeDTO updateDTO)
        {

            Perfume model = _mapper.Map<Perfume>(updateDTO);

            await _perfumeRepository.UpdateAsync(model);
        }

    }


}
