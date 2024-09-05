using CompanyStruct.Models;

namespace CompanyStruct.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int companyId);
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(int companyId);
        Task<bool> IsUsedAsync(int companyId);
    }
}
