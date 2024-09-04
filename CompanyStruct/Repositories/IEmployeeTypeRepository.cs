using CompanyStruct.Models;

namespace CompanyStruct.Repositories
{
    public interface IEmployeeTypeRepository
    {
        Task<IEnumerable<EmployeeType>> GetAllAsync();
        Task<EmployeeType?> GetByIdAsync(int typeId);
        Task AddAsync(EmployeeType employeeType);
        Task UpdateAsync(EmployeeType employeeType);
        Task DeleteAsync(int typeId);
        Task<bool> IsUsedAsync(int typeId);
    }
}
