using CompanyStruct.Data;
using CompanyStruct.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyStruct.Repositories
{
    public class DivisionRepository(CompanyDbContext context) : IDivisionRepository
    {
        private readonly CompanyDbContext _context = context;

        public async Task<IEnumerable<Division>> GetAllAsync()
        {
            return await _context.Divisions.ToListAsync();
        }

        public async Task<Division?> GetByIdAsync(int divisionId)
        {
            return await _context.Divisions.FindAsync(divisionId);
        }

        public async Task AddAsync(Division division)
        {
            await _context.Divisions.AddAsync(division);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Division division)
        {
            _context.Divisions.Update(division);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int divisionId)
        {
            var division = await _context.Divisions.FindAsync(divisionId);
            _context.Divisions.Remove(division);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUsedAsync(int divisionId)
        {
            return await _context.Projects.AnyAsync(c => c.DivisionId == divisionId);
        }
    }
}
