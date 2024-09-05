using CompanyStruct.Models;
using CompanyStruct.Repositories;

namespace CompanyStruct.Services
{
    public class ProjectService(IProjectRepository projectRepository, IEmployeeRepository employeeRepository) : IProjectService
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

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
                return (false, new List<string> { $"Project Id {project.Id} is already used." });
            }

            await _projectRepository.AddAsync(project);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int projectId, Project project)
        {
            var existingProject = await _projectRepository.GetByIdAsync(projectId);

            if (existingProject == null)
            {
                return (false, new List<string> { "Project not found." });
            }

            bool isUsed = await _projectRepository.IsUsedAsync(projectId);

            if (existingProject.Id != project.Id && isUsed)
            {
                return (false, new List<string> { "Cannot update project." });
            }

            var (isValid, errors) = await IsValidProject(project);
            if (!isValid)
            {
                return (false, errors);
            }

            existingProject.Name = project.Name;
            existingProject.Code = project.Code;
            existingProject.Head = project.Head;

            await _projectRepository.UpdateAsync(existingProject);
            return (true, new List<string>());
        }

        public async Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int projectId)
        {
            var division = await _projectRepository.GetByIdAsync(projectId);

            if (division == null)
            {
                return (false, new List<string> { "Project not found." });
            }

            bool isUsed = await _projectRepository.IsUsedAsync(projectId);

            if (isUsed)
            {
                return (false, new List<string> { "Cannot delete project." });
            }

            await _projectRepository.DeleteAsync(projectId);
            return (true, new List<string>());
        }

        private async Task<(bool IsValid, IList<string> Errors)> IsValidProject(Project project)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(project.Name))
            {
                errors.Add("Property Name is required and cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(project.Code))
            {
                errors.Add("Property Code is required and cannot be empty.");
            }

            var employeeExists = await _employeeRepository.GetByIdAsync(project.Head);

            if (employeeExists == null)
            {
                errors.Add($"Employee ID {project.Head} does not exist.");
            }

            if (project.Id <= 0)
            {
                errors.Add("Property ID must be a positive integer.");
            }

            return (errors.Count == 0, errors);
        }
    }
}
