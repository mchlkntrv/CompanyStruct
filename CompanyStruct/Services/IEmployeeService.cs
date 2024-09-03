using CompanyStruct.Models;

namespace CompanyStruct.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int employeeId);
        Task AddAsync(Employee employee);
        Task<bool> UpdateAsync(int employeeId, Employee employee);
        Task<bool> DeleteAsync(int employeeId);
    }
}
