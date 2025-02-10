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
        public async Task<ActionResult<IEnumerable<PerfumeDTO>>> GetPerfumes()
        {
            IEnumerable<PerfumeDTO> perfumeList = await _perfumeCase.GetAllPerfumes();
            return Ok(perfumeList);
        }

    }
}
