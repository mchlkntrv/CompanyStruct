using CompanyStruct.Models;

namespace CompanyStruct.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Boolean> DeleteEmployeeByIdAsync(int id);
    }
}
