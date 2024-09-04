using CompanyStruct.Models;
using CompanyStruct.Repositories;

namespace CompanyStruct.Services
{
    public class EmployeeTypeService(IEmployeeTypeRepository employeeTypeRepository) : IEmployeeTypeService
    {
        private readonly IEmployeeTypeRepository _employeeTypeRepository = employeeTypeRepository;
        public async Task<IEnumerable<EmployeeType>> GetAllAsync()
        {
            return await _employeeTypeRepository.GetAllAsync();
        }

        public async Task<EmployeeType?> GetByIdAsync(int typeId)
        {
            return await _employeeTypeRepository.GetByIdAsync(typeId);
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> AddAsync(EmployeeType employeeType)
        {
            var (isValid, errors) = IsValidEmployeeType(employeeType);
            if (!isValid)
            {
                return (false, errors);
            }

            if (await _employeeTypeRepository.GetByIdAsync(employeeType.Id) != null)
            {
                return (false, new List<string> { "Employee type already exists." });
            }

            await _employeeTypeRepository.AddAsync(employeeType);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int typeId, EmployeeType employeeType)
        {
            var existingEmployeeType = await _employeeTypeRepository.GetByIdAsync(typeId);

            if (existingEmployeeType == null)
            {
                return (false, new List<string> { "Employee type not found." });
            }

            if (await _employeeTypeRepository.IsUsedAsync(existingEmployeeType.Id) && existingEmployeeType.Id != employeeType.Id)
            {
                return (false, new List<string> { "Cannot update employee type Id as one or more employees have it listed." });
            }

            var (isValid, errors) = IsValidEmployeeType(employeeType);

            if (!isValid)
            {
                return (false, errors);
            }

            existingEmployeeType.Id = employeeType.Id;
            existingEmployeeType.TypeName = employeeType.TypeName;

            await _employeeTypeRepository.UpdateAsync(existingEmployeeType);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int typeId)
        {
            var employeeType = await _employeeTypeRepository.GetByIdAsync(typeId);

            if (employeeType == null)
            {
                return (false, new List<string> { "Employee type not found." });
            }

            bool isUsed = await _employeeTypeRepository.IsUsedAsync(typeId);

            if (isUsed)
            {
                return (false, new List<string> { "Cannot delete the type as one or more employees have it listed." });
            }

            await _employeeTypeRepository.DeleteAsync(typeId);
            return (true, new List<string>());
        }

        private static (bool IsValid, IList<string> Errors) IsValidEmployeeType(EmployeeType employeeType)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(employeeType.TypeName))
            {
                errors.Add("Property TypeName is required and cannot be empty.");
            }

            return (errors.Count == 0, errors);
        }
    }
}
