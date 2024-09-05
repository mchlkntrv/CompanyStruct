using CompanyStruct.Models;
using CompanyStruct.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStruct.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController(ICompanyService companyService) : ControllerBase
    {
        private readonly ICompanyService _companyService = companyService;

        // GET: api/companies/all
        // Get all companies
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Company>>> GetAllCompanies()
        {
            var companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        // GET: api/companies/{companyId}
        // Get company by ID
        [HttpGet("{companyId:int}")]
        public async Task<ActionResult<Employee>> GetCompanyById(int companyId)
        {
            var company = await _companyService.GetByIdAsync(companyId);

            if (company == null)
            {
                return NotFound("Company not found");
            }

            return Ok(company);
        }

        // POST: api/companies/add
        // Add new company
        [HttpPost("add")]
        public async Task<ActionResult> AddCompany([FromBody] Company company)
        {
            var (isSuccess, errors) = await _companyService.AddAsync(company);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok("Company added");
        }

        // PUT: api/companies/update/{companyId}
        // Update company by ID
        [HttpPut("update/{companyId:int}")]
        public async Task<IActionResult> UpdateCompany(int companyId, Company company)
        {
            var (isSuccess, errors) = await _companyService.UpdateAsync(companyId, company);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok("Company updated");
        }

        // DELETE: api/companies/delete/{companyId}
        // Delete company by ID
        [HttpDelete("delete/{companyId:int}")]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            var (isSuccess, errors) = await _companyService.DeleteAsync(companyId);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok("Company deleted");
        }
    }
}
