using CompanyStruct.Models;
using CompanyStruct.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStruct.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentController(IDepartmentService departmentService) : ControllerBase
    {
        private readonly IDepartmentService _departmentService = departmentService;

        // GET: api/departments/all
        // Get all departments
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }

        // GET: api/departments/{departmentId}
        // Get department by ID
        [HttpGet("{departmentId:int}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int departmentId)
        {
            var department = await _departmentService.GetByIdAsync(departmentId);

            if (department == null)
            {
                return NotFound("Department not found");
            }

            return Ok(department);
        }

        // POST: api/departments/add
        // Add new department
        [HttpPost("add")]
        public async Task<ActionResult<Department>> AddDepartment([FromBody] Department department)
        {
            var (isSuccess, errors) = await _departmentService.AddAsync(department);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }
            return Ok("Department added");
        }

        // PUT: api/departments/update/{departmentId}
        // Update department by ID
        [HttpPut("update/{departmentId:int}")]
        public async Task<IActionResult> UpdateDepartment(int departmentId, Department department)
        {
            var (isSuccess, errors) = await _departmentService.UpdateAsync(departmentId, department);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok("Department updated");
        }

        // DELETE: api/departments/delete/{departmentId}
        // Delete department by ID
        [HttpDelete("delete/{departmentId:int}")]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            var (isSuccess, errors) = await _departmentService.DeleteAsync(departmentId);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok("Department deleted");
        }
    }
}
