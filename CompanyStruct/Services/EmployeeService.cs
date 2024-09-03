using CompanyStruct.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace CompanyStruct.Services
{
    public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee?> GetByIdAsync(int employeeId)
        {
            return await _employeeRepository.GetByIdAsync(employeeId);
        }

        public async Task AddAsync(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
        }

        public async Task<bool> UpdateAsync(int employeeId, Employee employee)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(employeeId);

            if (existingEmployee != null) {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Title = employee.Title;
                existingEmployee.TypeId = employee.TypeId;
                await _employeeRepository.UpdateAsync(existingEmployee);
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);

            if (employee != null)
            {
                bool isHead = await _employeeRepository.IsHeadAsync(employeeId);

                if (isHead)
                {
                    throw new InvalidOperationException("Cannot delete employee as they are a head of a company, division, department, or project.");
                }

                await _employeeRepository.DeleteAsync(employeeId);
                return true;
            }
            return false;
        }
    }
}
