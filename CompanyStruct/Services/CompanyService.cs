using CompanyStruct.Models;
using CompanyStruct.Repositories;

namespace CompanyStruct.Services
{
    public class CompanyService(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository) : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _companyRepository.GetAllAsync();
        }

        public async Task<Company?> GetByIdAsync(int companyId)
        {
            return await _companyRepository.GetByIdAsync(companyId);
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> AddAsync(Company company)
        {
            var (isValid, errors) = await IsValidCompany(company);
            if (!isValid)
            {
                return (false, errors);
            }

            if (await _companyRepository.GetByIdAsync(company.Id) != null)
            {
                return (false, new List<string> { $"Company ID {company.Id} is already used" });
            }

            await _companyRepository.AddAsync(company);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int companyId, Company company)
        {
            var existingCompany = await _companyRepository.GetByIdAsync(companyId);

            if (existingCompany == null)
            {
                return (false, new List<string> { "Company not found" });
            }

            bool isUsed = await _companyRepository.IsUsedAsync(companyId);

            if (existingCompany.Id != company.Id || isUsed)
            {
                return (false, new List<string> { "Cannot update company" });
            }

            var (isValid, errors) = await IsValidCompany(company);
            if (!isValid)
            {
                return (false, errors);
            }

            existingCompany.Name = company.Name;
            existingCompany.Code = company.Code;
            existingCompany.Head = company.Head;

            await _companyRepository.UpdateAsync(existingCompany);
            return (true, new List<string>());
        }
        public async Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);

            if (company == null)
            {
                return (false, new List<string> { "Company not found" });
            }

            bool isUsed = await _companyRepository.IsUsedAsync(companyId);

            if (isUsed)
            {
                return (false, new List<string> { "Cannot delete company" });
            }

            await _companyRepository.DeleteAsync(companyId);
            return (true, new List<string>());
        }

        private async Task<(bool IsValid, IList<string> Errors)> IsValidCompany(Company company)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(company.Name))
            {
                errors.Add("Name is required and cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(company.Code))
            {
                errors.Add("Code is required and cannot be empty");
            }

            var employeeExists = await _employeeRepository.GetByIdAsync(company.Head);

            if (employeeExists == null)
            {
                errors.Add($"Employee ID {company.Head} does not exist");
            }

            if (company.Id <= 0)
            {
                errors.Add("Property Id must be a positive integer");
            }

            return (errors.Count == 0, errors);
        }
    }
}
