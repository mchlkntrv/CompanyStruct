using CompanyStruct.Models;
using CompanyStruct.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStruct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController(IDivisionService divisionService) : ControllerBase
    {
        private readonly IDivisionService _divisionService = divisionService;

        //Get all divisions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Division>>> GetAllDivisions()
        {
            var divisions = await _divisionService.GetAllAsync();
            return Ok(divisions);
        }

        //Get division by ID
        [HttpGet("{divisionId}")]
        public async Task<ActionResult<Division>> GetDivisionById(int divisionId)
        {
            var company = await _divisionService.GetByIdAsync(divisionId);

            if (company == null)
            {
                return NotFound($"Division ID {divisionId} not found");
            }

            return Ok(company);
        }

        //Add new division
        [HttpPost]
        public async Task<ActionResult<Division>> AddDivision(Division division)
        {
            var (isSuccess, errors) = await _divisionService.AddAsync(division);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }
            return Ok($"Division ID {division.Id} ADDED");
        }

        //Update division by ID
        [HttpPut("{divisionId}")]
        public async Task<IActionResult> UpdateDivision(int divisionId, Division division)
        {
            var (isSuccess, errors) = await _divisionService.UpdateAsync(divisionId, division);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Division ID {divisionId} UPDATED");
        }

        //Delete division by ID
        [HttpDelete("{divisionId}")]
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
