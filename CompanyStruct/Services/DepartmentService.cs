using CompanyStruct.Models;
using CompanyStruct.Repositories;

namespace CompanyStruct.Services
{
    public class DepartmentService(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository, IProjectRepository projectRepository) : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IProjectRepository _projectRepository = projectRepository;

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
                return (false, new List<string> { $"Department ID {department.Id} is already used" });
            }

            await _departmentRepository.AddAsync(department);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int departmentId, Department department)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(departmentId);

            if (existingDepartment == null)
            {
                return (false, new List<string> { "Department not found" });
            }

            if (existingDepartment.Id != department.Id)
            {
                return (false, new List<string> { "Cannot change department ID" });
            }

            var (isValid, errors) = await IsValidDepartment(department);
            if (!isValid)
            {
                return (false, errors);
            }

            existingDepartment.Name = department.Name;
            existingDepartment.Code = department.Code;
            existingDepartment.Head = department.Head;
            existingDepartment.ProjectId = department.ProjectId;

            await _departmentRepository.UpdateAsync(existingDepartment);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int departmentId)
        {
            var division = await _departmentRepository.GetByIdAsync(departmentId);

            if (division == null)
            {
                return (false, new List<string> { "Department not found" });
            }

            await _departmentRepository.DeleteAsync(departmentId);
            return (true, new List<string>());
        }

        private async Task<(bool IsValid, IList<string> Errors)> IsValidDepartment(Department department)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(department.Name))
            {
                errors.Add("Name is required and cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(department.Code))
            {
                errors.Add("Code is required and cannot be empty");
            }

            var employee = await _employeeRepository.GetByIdAsync(department.Head);

            if (employee == null)
            {
                errors.Add($"Employee ID {department.Head} does not exist");
            }
            else if (employee.TypeId != EmployeeTypeConstants.DEPARTMENT_HEAD_TYPEID)
            {
                errors.Add($"Employee ID {department.Head} with Employee Type ID {employee.TypeId} can not be head of Department. Required Employee Type ID is {EmployeeTypeConstants.DEPARTMENT_HEAD_TYPEID}");
            }

            if (department.Id <= 0)
            {
                errors.Add("ID must be a positive integer");
            }

            var project = await _projectRepository.GetByIdAsync(department.ProjectId);

            if (project == null)
            {
                errors.Add($"Project ID {department.ProjectId} does not exist");
            }

            return (errors.Count == 0, errors);
        }
    }
}