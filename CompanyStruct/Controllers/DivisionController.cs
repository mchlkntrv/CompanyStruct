using CompanyStruct.Models;
using CompanyStruct.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStruct.Controllers
{
    [Route("api/divisions")]
    [ApiController]
    public class DivisionController(IDivisionService divisionService) : ControllerBase
    {
        private readonly IDivisionService _divisionService = divisionService;

        // GET: api/divisions/all
        // Get all divisions
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Division>>> GetAllDivisions()
        {
            var divisions = await _divisionService.GetAllAsync();
            return Ok(divisions);
        }

        // GET: api/divisions/{divisionId}
        // Get division by ID
        [HttpGet("{divisionId:int}")]
        public async Task<ActionResult<Division>> GetDivisionById(int divisionId)
        {
            var division = await _divisionService.GetByIdAsync(divisionId);

            if (division == null)
            {
                return NotFound($"Division ID {divisionId} not found");
            }

            return Ok(division);
        }

        // POST: api/divisions/add
        // Add new division
        [HttpPost("add")]
        public async Task<ActionResult<Division>> AddDivision([FromBody] Division division)
        {
            var (isSuccess, errors) = await _divisionService.AddAsync(division);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }
            return Ok($"Division ID {division.Id} ADDED");
        }

        // PUT: api/divisions/update/{divisionId}
        // Update division by ID
        [HttpPut("update/{divisionId:int}")]
        public async Task<IActionResult> UpdateDivision(int divisionId, Division division)
        {
            var (isSuccess, errors) = await _divisionService.UpdateAsync(divisionId, division);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Division ID {divisionId} UPDATED");
        }

        // DELETE: api/divisions/delete/{divisionId}
        // Delete division by ID
        [HttpDelete("delete/{divisionId:int}")]
        public async Task<IActionResult> DeleteDivision(int divisionId)
        {
            var (isSuccess, errors) = await _divisionService.DeleteAsync(divisionId);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Division {divisionId} DELETED");
        }
    }
}
