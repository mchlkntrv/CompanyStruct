using CompanyStruct.Models;
using CompanyStruct.Repositories;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<(bool IsSuccess, IList<string> Errors)> AddAsync(Employee employee)
        {
            var (isValid, errors) = ValidateEmployee(employee);
            if (!isValid)
            {
                return (false, errors);
            }

            await _employeeRepository.AddAsync(employee);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int employeeId, Employee employee)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(employeeId);

            if (existingEmployee == null)
            {
                return (false, new List<string> { "Employee not found." });
            }

            bool isHead = await _employeeRepository.IsHeadAsync(employeeId);

            if (existingEmployee.TypeId != employee.TypeId && isHead)
            {
                return (false, new List<string> { "Cannot update employee type as they are a head of a company, division, department, or project." });
            }

            var (isValid, errors) = ValidateEmployee(employee);
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

        private static (bool IsValid, IList<string> Errors) ValidateEmployee(Employee employee)
        {
            //TODO: SKONTROLOVAT MESSAGE V ERROROCH
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(employee.FirstName))
            {
                errors.Add("Property FirstName is required and cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(employee.LastName))
            {
                errors.Add("Property LastName is required and cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(employee.Phone))
            {
                errors.Add("Property Phone is required and cannot be empty.");
            }

            if (!IsValidEmail(employee.Email))
            {
                errors.Add("Property Email is required and must be a valid email address.");
            }
            //TODO: DOPLNIT ESTE ABY SA DALI DOPLNIT LEN TYPEID KTORE SKUTOCNE EXISTUJU!!
            if (employee.TypeId <= 0)
            {
                errors.Add("Property TypeId must be a positive integer.");
            }

            return (errors.Count == 0, errors);
        }
        private static bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && MailAddress.TryCreate(email, out _);
        }
    }
}
