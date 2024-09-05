using CompanyStruct.Data;
using CompanyStruct.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyStruct.Repositories
{
    public class EmployeeRepository(CompanyDbContext context) : IEmployeeRepository
    {
        private readonly CompanyDbContext _context = context;

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);

            if (employee == null)
            {
                throw new ArgumentException($"Employee with ID {employeeId} not found");
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsHeadAsync(int employeeId)
        {
            bool isHead = await _context.Companies.AnyAsync(c => c.Head == employeeId) ||
                          await _context.Divisions.AnyAsync(d => d.Head == employeeId) ||
                          await _context.Departments.AnyAsync(dep => dep.Head == employeeId) ||
                          await _context.Projects.AnyAsync(p => p.Head == employeeId);

            return isHead;
        }
    }
}
