using CompanyStruct.Data;
using CompanyStruct.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyStruct.Repositories
{
    public class CompanyRepository(CompanyDbContext context) : ICompanyRepository
    {
        private readonly CompanyDbContext _context = context;

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int companyId)
        {
            return await _context.Companies.FindAsync(companyId);
        }

        public async Task AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUsedAsync(int companyId)
        {
            return await _context.Divisions.AnyAsync(c => c.CompanyId == companyId);
        }
    }
}
