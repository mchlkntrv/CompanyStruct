using CompanyStruct.Models;

namespace CompanyStruct.Services
{
    public interface IEmployeeTypeService
    {
        Task<IEnumerable<EmployeeType>> GetAllAsync();
        Task<EmployeeType?> GetByIdAsync(int typeId);
        Task<(bool IsSuccess, IList<string> Errors)> AddAsync(EmployeeType employeeType);
        Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int typeId, EmployeeType employeeType);
        Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int typeId);
    }
}
