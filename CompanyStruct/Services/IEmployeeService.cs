using CompanyStruct.Models;

namespace CompanyStruct.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int employeeId);
        Task<(bool IsSuccess, IList<string> Errors)> AddAsync(Employee employee);
        Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int employeeId, Employee employee);
        Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int employeeId);
    }
}
