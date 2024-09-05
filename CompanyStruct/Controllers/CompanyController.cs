using CompanyStruct.Models;
using CompanyStruct.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStruct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(ICompanyService companyService) : ControllerBase
    {
        private readonly ICompanyService _companyService = companyService;

        //Get all companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAllCompanies()
        {
            var companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        //Get company by ID
        [HttpGet("{companyId}")]
        public async Task<ActionResult<Employee>> GetCompanyById(int companyId)
        {
            var company = await _companyService.GetByIdAsync(companyId);

            if (company == null)
            {
                return NotFound($"Company ID {companyId} not found");
            }

            return Ok(company);
        }

        //Add new company
        [HttpPost]
        public async Task<ActionResult<Division>> AddCompany(Company company)
        {
            var (isSuccess, errors) = await _companyService.AddAsync(company);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }
            return Ok($"Company ID {company.Id} ADDED");
        }

        //Update company by ID
        [HttpPut("{companyId}")]
        public async Task<IActionResult> UpdateCompany(int companyId, Company company)
        {
            var (isSuccess, errors) = await _companyService.UpdateAsync(companyId, company);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Company ID {companyId} UPDATED");
        }

        //Delete company by ID
        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            var (isSuccess, errors) = await _companyService.DeleteAsync(companyId);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Company {companyId} DELETED");
        }
    }
}
