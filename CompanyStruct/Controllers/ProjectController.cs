using CompanyStruct.Models;
using CompanyStruct.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStruct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        //Get all projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

        //Get project by ID
        [HttpGet("{projectId}")]
        public async Task<ActionResult<Division>> GetProjectById(int projectId)
        {
            var project = await _projectService.GetByIdAsync(projectId);

            if (project == null)
            {
                return NotFound($"Project ID {projectId} not found");
            }

            return Ok(project);
        }

        //Add new project
        [HttpPost]
        public async Task<ActionResult<Project>> AddProject(Project project)
        {
            var (isSuccess, errors) = await _projectService.AddAsync(project);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }
            return Ok($"Project ID {project.Id} ADDED");
        }

        //Update project by ID
        [HttpPut("{projectId}")]
        public async Task<IActionResult> UpdateProject(int projectId, Project project)
        {
            var (isSuccess, errors) = await _projectService.UpdateAsync(projectId, project);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Project ID {projectId} UPDATED");
        }

        //Delete project by ID
        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var (isSuccess, errors) = await _projectService.DeleteAsync(projectId);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok($"Project {projectId} DELETED");
        }
    }
}
