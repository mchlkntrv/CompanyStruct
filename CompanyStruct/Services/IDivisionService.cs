using CompanyStruct.Models;

namespace CompanyStruct.Services
{
    public interface IDivisionService
    {
        Task<IEnumerable<Division>> GetAllAsync();
        Task<Division?> GetByIdAsync(int divisionId);
        Task<(bool IsSuccess, IList<string> Errors)> AddAsync(Division division);
        Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int divisionId, Division division);
        Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int divisionId);
    }
}
