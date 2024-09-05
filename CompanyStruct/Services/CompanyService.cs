using CompanyStruct.Models;
using CompanyStruct.Repositories;

namespace CompanyStruct.Services
{
    public class CompanyService(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, IEmployeeTypeRepository employeeTypeRepository) : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IEmployeeTypeRepository _employeeTypeRepository = employeeTypeRepository;

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

            if (existingCompany.Id != company.Id && isUsed)
            {
                return (false, new List<string> { "Cannot change company ID" });
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
                return (false, new List<string> { "Cannot delete company as it is being used" });
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

            var employee = await _employeeRepository.GetByIdAsync(company.Head);

            if (employee == null)
            {
                errors.Add($"Employee ID {company.Head} does not exist");
            }
            else if (employee.TypeId != EmployeeTypeConstants.COMPANY_HEAD_TYPEID)
            {
                errors.Add($"Employee ID {company.Head} with Employee Type ID {employee.TypeId} can not be head of Company. Required Employee Type ID is {EmployeeTypeConstants.COMPANY_HEAD_TYPEID}");
            }

            if (company.Id <= 0)
            {
                errors.Add("Property Id must be a positive integer");
            }

            return (errors.Count == 0, errors);
        }
    }
}
