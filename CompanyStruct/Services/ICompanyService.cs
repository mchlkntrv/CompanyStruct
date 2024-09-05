using CompanyStruct.Models;

namespace CompanyStruct.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int companyId);
        Task<(bool IsSuccess, IList<string> Errors)> AddAsync(Company company);
        Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int companyId, Company company);
        Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int companyId);
    }
}
