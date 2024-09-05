using CompanyStruct.Models;
using CompanyStruct.Repositories;

namespace CompanyStruct.Services
{
    public class ProjectService(IProjectRepository projectRepository, IEmployeeRepository employeeRepository, IDivisionRepository divisionRepository) : IProjectService
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IDivisionRepository _divisionRepository = divisionRepository;

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task<Project?> GetByIdAsync(int projectId)
        {
            return await _projectRepository.GetByIdAsync(projectId);
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> AddAsync(Project project)
        {
            var (isValid, errors) = await IsValidProject(project);
            if (!isValid)
            {
                return (false, errors);
            }

            if (await _projectRepository.GetByIdAsync(project.Id) != null)
            {
                return (false, new List<string> { $"Project ID {project.Id} is already used" });
            }

            await _projectRepository.AddAsync(project);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int projectId, Project project)
        {
            var existingProject = await _projectRepository.GetByIdAsync(projectId);

            if (existingProject == null)
            {
                return (false, new List<string> { "Project not found" });
            }

            bool isUsed = await _projectRepository.IsUsedAsync(projectId);

            if (existingProject.Id != project.Id && isUsed)
            {
                return (false, new List<string> { "Cannot change project ID" });
            }

            var (isValid, errors) = await IsValidProject(project);
            if (!isValid)
            {
                return (false, errors);
            }

            existingProject.Name = project.Name;
            existingProject.Code = project.Code;
            existingProject.Head = project.Head;
            existingProject.DivisionId = project.DivisionId;

            await _projectRepository.UpdateAsync(existingProject);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int projectId)
        {
            var division = await _projectRepository.GetByIdAsync(projectId);

            if (division == null)
            {
                return (false, new List<string> { "Project not found" });
            }

            bool isUsed = await _projectRepository.IsUsedAsync(projectId);

            if (isUsed)
            {
                return (false, new List<string> { "Cannot delete project as it is being used" });
            }

            await _projectRepository.DeleteAsync(projectId);
            return (true, new List<string>());
        }

        private async Task<(bool IsValid, IList<string> Errors)> IsValidProject(Project project)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(project.Name))
            {
                errors.Add("Name is required and cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(project.Code))
            {
                errors.Add("Code is required and cannot be empty");
            }

            var employee = await _employeeRepository.GetByIdAsync(project.Head);

            if (employee == null)
            {
                errors.Add($"Employee ID {project.Head} does not exist");
            }
            else if (employee.TypeId != EmployeeTypeConstants.PROJECT_HEAD_TYPEID)
            {
                errors.Add($"Employee ID {project.Head} with Employee Type ID {employee.TypeId} can not be head of Project. Required Employee Type ID is {EmployeeTypeConstants.PROJECT_HEAD_TYPEID}");
            }

            if (project.Id <= 0)
            {
                errors.Add("ID must be a positive integer");
            }

            var division = await _divisionRepository.GetByIdAsync(project.DivisionId);

            if (division == null)
            {
                errors.Add($"Division ID {project.DivisionId} does not exist");
            }

            return (errors.Count == 0, errors);
        }
    }
}
