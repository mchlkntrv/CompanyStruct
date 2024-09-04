using CompanyStruct.Data;
using CompanyStruct.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyStruct.Repositories
{
    public class EmployeeTypeRepository(CompanyDbContext context) : IEmployeeTypeRepository
    {
        private readonly CompanyDbContext _context = context;
        public async Task<IEnumerable<EmployeeType>> GetAllAsync()
        {
            return await _context.EmployeeTypes.ToListAsync();
        }
        public async Task<EmployeeType?> GetByIdAsync(int typeId)
        {
            return await _context.EmployeeTypes.FindAsync(typeId);
        }

        public async Task AddAsync(EmployeeType employeeType)
        {
            await _context.EmployeeTypes.AddAsync(employeeType);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(EmployeeType employeeType)
        {
            _context.EmployeeTypes.Update(employeeType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int typeId)
        {
            var employeeType = await _context.EmployeeTypes.FindAsync(typeId);
            if (employeeType != null)
            {
                _context.EmployeeTypes.Remove(employeeType);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> IsUsedAsync(int typeId)
        {
            bool isUsed = await _context.Employees.AnyAsync(c => c.TypeId == typeId);
            return isUsed;
        }
    }
}
