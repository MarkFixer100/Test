
using Application.StudentDTOS;
using Application.Use_Case;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    [Route("api/StudentAPI")]
    [ApiController]
    public class StudentAPI : ControllerBase
    {
        private readonly StudentCase _studentCase;
        public StudentAPI(StudentCase studentCase) {
            
            _studentCase = studentCase;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetAll()
        {
            IEnumerable<StudentDTO> students = await _studentCase.GetAllStudents();

            return Ok(students);
        }
        [HttpGet("{id:Guid}", Name = "GetSudent")]

        public async Task<ActionResult<StudentDTO>> GetStudentById(Guid id)
        {
           var student = await _studentCase.GetById(id);

            return Ok(student);
        }

        [HttpPost]

        public async Task CreateStudent([FromBody] CreateStudentDTO createStudent)
        {
            await _studentCase.AddAsync(createStudent);
        }

        [HttpPut("{id:Guid}", Name = "UpdateStudent")]

        public async Task<IActionResult> UpdateStudent(Guid id , [FromBody] UpdateStudentDTO updateStudent)
        {
            if (updateStudent == null || id != updateStudent.Id)
            {
                return BadRequest();
            }
            await _studentCase.UpdateAsync(id, updateStudent);

            return NoContent();
        }


        [HttpDelete("{id:Guid}", Name = "DeleteStudent")]

        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            if(id == Guid.Empty)
            {
                return BadRequest();
            }

            await _studentCase.Delete(id);

            return NoContent();
        }

        [HttpGet("course/{СourseId}")]

        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentByCourse(Guid CourseId)
        {
            if (CourseId == Guid.Empty) {
                

                return BadRequest();
            }

             var studentsByCourse = await _studentCase.GetStudentsByCourseId(CourseId);

            if (studentsByCourse == null) {
                return NotFound();
            }

            return Ok(studentsByCourse);
        }
    }
}
