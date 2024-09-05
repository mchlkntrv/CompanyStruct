using CompanyStruct.Models;
using CompanyStruct.Repositories;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyStruct.Services
{
    public class DivisionService(IDivisionRepository divisionRepository, IEmployeeRepository employeeRepository, ICompanyRepository companyRepository) : IDivisionService
    {
        private readonly IDivisionRepository _divisionRepository = divisionRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly ICompanyRepository _companyRepository = companyRepository;

        public async Task<IEnumerable<Division>> GetAllAsync()
        {
            return await _divisionRepository.GetAllAsync();
        }

        public async Task<Division?> GetByIdAsync(int divisionId)
        {
            return await _divisionRepository.GetByIdAsync(divisionId);
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> AddAsync(Division division)
        {
            var (isValid, errors) = await IsValidDivision(division);
            if (!isValid)
            {
                return (false, errors);
            }

            if (await _divisionRepository.GetByIdAsync(division.Id) != null)
            {
                return (false, new List<string> { $"Division ID {division.Id} is already used" });
            }

            await _divisionRepository.AddAsync(division);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int divisionId, Division division)
        {
            var existingDivision = await _divisionRepository.GetByIdAsync(divisionId);

            if (existingDivision == null)
            {
                return (false, new List<string> { "Division not found" });
            }

            bool isUsed = await _divisionRepository.IsUsedAsync(divisionId);

            if (existingDivision.Id != division.Id && isUsed)
            {
                return (false, new List<string> { "Cannot change division ID" });
            }

            var (isValid, errors) = await IsValidDivision(division);
            if (!isValid)
            {
                return (false, errors);
            }

            existingDivision.Name = division.Name;
            existingDivision.Code = division.Code;
            existingDivision.Head = division.Head;
            existingDivision.CompanyId = division.CompanyId;

            await _divisionRepository.UpdateAsync(existingDivision);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int divisionId)
        {
            var division = await _divisionRepository.GetByIdAsync(divisionId);

            if (division == null)
            {
                return (false, new List<string> { "Division not found" });
            }

            bool isUsed = await _divisionRepository.IsUsedAsync(divisionId);

            if (isUsed)
            {
                return (false, new List<string> { "Cannot delete division as it is being used" });
            }

            await _divisionRepository.DeleteAsync(divisionId);
            return (true, new List<string>());
        }

        private async Task<(bool IsValid, IList<string> Errors)> IsValidDivision(Division division)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(division.Name))
            {
                errors.Add("Name is required and cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(division.Code))
            {
                errors.Add("Code is required and cannot be empty");
            }

            var employeeExists = await _employeeRepository.GetByIdAsync(division.Head);

            if (employeeExists == null)
            {
                errors.Add($"Employee ID {division.Head} does not exist");
            }
            else if (employeeExists.TypeId != 2)
            {
                errors.Add($"Employee ID {division.Head} with Employee Type ID {employeeExists.TypeId} can not be head of Division. Required Employee Type ID is 2");
            }

            if (division.Id <= 0)
            {
                errors.Add("ID must be a positive integer");
            }

            var companyExists = await _companyRepository.GetByIdAsync(division.CompanyId);

            if (companyExists == null)
            {
                errors.Add($"Company ID {division.CompanyId} does not exist");
            }

            return (errors.Count == 0, errors);
        }
    }
}
