using CompanyStruct.Models;
using CompanyStruct.Repositories;

namespace CompanyStruct.Services
{
    public class DepartmentService(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository) : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<Department?> GetByIdAsync(int departmentId)
        {
            return await _departmentRepository.GetByIdAsync(departmentId);
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> AddAsync(Department department)
        {
            var (isValid, errors) = await IsValidDepartment(department);
            if (!isValid)
            {
                return (false, errors);
            }

            if (await _departmentRepository.GetByIdAsync(department.Id) != null)
            {
                return (false, new List<string> { $"Project Id {department.Id} is already used." });
            }

            await _departmentRepository.AddAsync(department);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int departmentId, Department department)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(departmentId);

            if (existingDepartment == null)
            {
                return (false, new List<string> { "Project not found." });
            }

            var (isValid, errors) = await IsValidDepartment(department);
            if (!isValid)
            {
                return (false, errors);
            }

            existingDepartment.Name = department.Name;
            existingDepartment.Code = department.Code;
            existingDepartment.Head = department.Head;

            await _departmentRepository.UpdateAsync(existingDepartment);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int departmentId)
        {
            var division = await _departmentRepository.GetByIdAsync(departmentId);

            if (division == null)
            {
                return (false, new List<string> { "Department not found." });
            }

            await _departmentRepository.DeleteAsync(departmentId);
            return (true, new List<string>());
        }

        private async Task<(bool IsValid, IList<string> Errors)> IsValidDepartment(Department department)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(department.Name))
            {
                errors.Add("Property Name is required and cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(department.Code))
            {
                errors.Add("Property Code is required and cannot be empty.");
            }

            var employeeExists = await _employeeRepository.GetByIdAsync(department.Head);

            if (employeeExists == null)
            {
                errors.Add($"Employee ID {department.Head} does not exist.");
            }

            if (department.Id <= 0)
            {
                errors.Add("Property ID must be a positive integer.");
            }

            return (errors.Count == 0, errors);
        }
    }
}