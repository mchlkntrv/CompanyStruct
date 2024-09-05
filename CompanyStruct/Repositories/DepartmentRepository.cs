using CompanyStruct.Data;
using CompanyStruct.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyStruct.Repositories
{
    public class DepartmentRepository(CompanyDbContext context) : IDepartmentRepository
    {
        private readonly CompanyDbContext _context = context;

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int departmentId)
        {
            return await _context.Departments.FindAsync(departmentId);
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int departmentId)
        {
            var department = await _context.Departments.FindAsync(departmentId);

            if (department == null)
            {
                throw new ArgumentException($"Department with ID {departmentId} not found");
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
    }
}
