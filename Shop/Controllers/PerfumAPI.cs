using Application.Dto;
using Application.Use_Case;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
      [ApiController]
    [Route("api/PerfumeAPI")]
    public class PerfumAPI:ControllerBase
    {

        private readonly PerfumeCase _perfumeCase;
        
        public PerfumAPI(PerfumeCase perfumeCase)
        {
            _perfumeCase = perfumeCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfumeDTO>>> GetPerfumesAsync()
        {
            IEnumerable<PerfumeDTO> perfumeList = await _perfumeCase.GetAllPerfumesAsync();

            return Ok(perfumeList);
        }

        [HttpGet("{id:int}", Name = "GetPerfume")]

        public async Task<ActionResult<PerfumeDTO>> GetPerfumeAsync(int id)
        {
            if (id == 0) { 
                return BadRequest();
            }

            var perfume = await _perfumeCase.GetAsync(id);
            
            return Ok(perfume);
        }

        [HttpPost]

        public async Task CreatePerfume([FromBody] CreatePerfumeDTO createPerfume)
        {
           await _perfumeCase.AddAsync(createPerfume);
        }

        [HttpDelete("{id:int}", Name = "DeletePerfume")]

        public async Task<IActionResult> DeletePerfume(int id)
        {
            if (id == 0) {
            
                return BadRequest();
            }

           await _perfumeCase.Delete(id);

            return NoContent();

        }

        [HttpPut("{id:int}", Name = "Update")]

        public async Task<IActionResult> UpdatePerfumeAsync(int id , UpdatePerfumeDTO Updateperfume)
        {
            if (Updateperfume == null || id != Updateperfume.Id ) {
                return BadRequest(Updateperfume);
            }

            await _perfumeCase.UpdateAsync(id, Updateperfume);

            return NoContent();
        }    


    }
}
