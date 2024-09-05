using CompanyStruct.Models;

namespace CompanyStruct.Repositories
{
    public interface IDivisionRepository
    {
        Task<IEnumerable<Division>> GetAllAsync();
        Task<Division?> GetByIdAsync(int divisionId);
        Task AddAsync(Division division);
        Task UpdateAsync(Division division);
        Task DeleteAsync(int divisionId);
        Task<bool> IsUsedAsync(int divisionId);
    }
}
