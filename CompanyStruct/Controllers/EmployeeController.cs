using CompanyStruct.Models;
using CompanyStruct.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyStruct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;

        //Get all employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        //Get employee by ID
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int employeeId)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);

            if (employee == null)
            {
                return NotFound($"Employee ID {employeeId} not found");
            }

            return Ok(employee);
        }

        //Add new employee
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            await _employeeService.AddAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        //Update employee by ID
        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, Employee employee)
        {
            if (employeeId != employee.Id)
            {
                return BadRequest("Employee ID DOES NOT MATCH");
            }

            if (await _employeeService.UpdateAsync(employeeId, employee))
            {
                return Ok($"Employee ID {employeeId} UPDATED");
            }

            return BadRequest($"Employee ID {employeeId} NOT UPDATED");
        }

        //Delete employee by ID
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeById(int employeeId)
        {
            if (await _employeeService.DeleteAsync(employeeId))
            {
                return Ok($"Employee ID {employeeId} DELETED");
            }

            return BadRequest($"Employee ID {employeeId} NOT DELETED");
        }
    }
}