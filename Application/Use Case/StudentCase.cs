
using Application.StudentDTOS;
using AutoMapper;
using Domain.Entities;
using Domain.IReposotory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Case
{
    public class StudentCase
    {
        private readonly IStudentRepository _studentRepository;

        private readonly IMapper _mapper;

        public StudentCase(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudents()
        {
            var students = await _studentRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<StudentDTO> GetById(Guid id)
        {
            if(id == Guid.Empty)
            {
                return null;
            }
            var student = await _studentRepository.GetAsync(s => s.Id == id);

            return _mapper.Map<StudentDTO>(student);
            
        }

        public async Task<StudentDTO> AddAsync(CreateStudentDTO model)
        {
            Student student =  _mapper.Map<Student>(model);

            await _studentRepository.CreateAsync(student);

            return _mapper.Map<StudentDTO>(student);
        }

        public async Task Delete(Guid id)
        {
            var Student = await _studentRepository.GetAsync(p => p.Id == id);

            await _studentRepository.Remove(Student);

            await _studentRepository.SaveAsync();
        }

        public async Task UpdateAsync(Guid id, UpdateStudentDTO updateDTO)
        {

            Student model = _mapper.Map<Student>(updateDTO);

            await _studentRepository.UpdateAsync(model);
        }

       public async Task<IEnumerable<StudentDTO>> GetStudentsByCourseId(Guid courseId)
        {

            var students = await _studentRepository.GetAllAsync(u => u.CourseId == courseId);

            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

    }
}
