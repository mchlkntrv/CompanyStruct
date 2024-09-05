using CompanyStruct.Models;
using CompanyStruct.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStruct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IDepartmentService departmentService) : ControllerBase
    {
        private readonly IDepartmentService _departmentService = departmentService;

        //Get all departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }

        //Get department by ID
        [HttpGet("{departmentId}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int departmentId)
        {
            var department = await _departmentService.GetByIdAsync(departmentId);

            if (department == null)
            {
                return NotFound($"Department ID {departmentId} not found");
            }

            return Ok(department);
        }

        //Add new department
        [HttpPost]
        public async Task<ActionResult<Department>> AddDepartment(Department department)
        {
            var (isSuccess, errors) = await _departmentService.AddAsync(department);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }
            return Ok($"Department ID {department.Id} ADDED");
        }

        //Update department by ID
        [HttpPut("{departmentId}")]
        public async Task<IActionResult> UpdateDepartment(int departmentId, Department department)
        {
            var (isSuccess, errors) = await _departmentService.UpdateAsync(departmentId, department);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Department ID {departmentId} UPDATED");
        }

        //Delete department by ID
        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            var (isSuccess, errors) = await _departmentService.DeleteAsync(departmentId);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Department {departmentId} DELETED");
        }
    }
}
