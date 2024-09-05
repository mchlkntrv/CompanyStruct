using CompanyStruct.Models;
using CompanyStruct.Repositories;
using System.Net.Mail;

namespace CompanyStruct.Services
{
    public class EmployeeService(IEmployeeRepository employeeRepository, IEmployeeTypeRepository employeeTypeRepository) : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IEmployeeTypeRepository _employeeTypeRepository = employeeTypeRepository;

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee?> GetByIdAsync(int employeeId)
        {
            return await _employeeRepository.GetByIdAsync(employeeId);
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> AddAsync(Employee employee)
        {
            var (isValid, errors) = await IsValidEmployee(employee);
            if (!isValid)
            {
                return (false, errors);
            }

            if (await _employeeRepository.GetByIdAsync(employee.Id) != null)
            {
                return (false, new List<string> { $"Employee ID {employee.Id} is already used" });
            }

            await _employeeRepository.AddAsync(employee);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int employeeId, Employee employee)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(employeeId);

            if (existingEmployee == null)
            {
                return (false, new List<string> { "Employee not found" });
            }

            bool isHead = await _employeeRepository.IsHeadAsync(employeeId);

            if (isHead && existingEmployee.TypeId != employee.TypeId)
            {
                return (false, new List<string> { "Cannot change employee type as they are head of either company, department, project or division" });
            }

            if (existingEmployee.Id != employee.Id)
            {
                return (false, new List<string> { "Cannot change employee ID" });
            }

            var (isValid, errors) = await IsValidEmployee(employee);
            if (!isValid)
            {
                return (false, errors);
            }

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.Title = employee.Title;
            existingEmployee.TypeId = employee.TypeId;

            await _employeeRepository.UpdateAsync(existingEmployee);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int employeeId)
        {
            var employeeType = await _employeeRepository.GetByIdAsync(employeeId);

            if (employeeType == null)
            {
                return (false, new List<string> { "Employee not found" });
            }

            bool isHead = await _employeeRepository.IsHeadAsync(employeeId);

            if (isHead)
            {
                return (false, new List<string> { "Cannot delete employee as they are head of either company, department, project or division" });
            }

            await _employeeRepository.DeleteAsync(employeeId);
            return (true, new List<string>());

        }

        private async Task<(bool IsValid, IList<string> Errors)> IsValidEmployee(Employee employee)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(employee.FirstName))
            {
                errors.Add("FirstName is required and cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(employee.LastName))
            {
                errors.Add("LastName is required and cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(employee.Phone))
            {
                errors.Add("Phone is required and cannot be empty");
            }

            if (!IsValidEmail(employee.Email))
            {
                errors.Add("Email is required and must be a valid email address");
            }

            var employeeTypeExists = await _employeeTypeRepository.GetByIdAsync(employee.TypeId);

            if (employeeTypeExists == null)
            {
                errors.Add($"Employee type ID {employee.TypeId} does not exist");
            }

            if (employee.TypeId <= 0)
            {
                errors.Add("TypeId must be a positive integer");
            }

            return (errors.Count == 0, errors);
        }

        private static bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && MailAddress.TryCreate(email, out _);
        }
    }
}
