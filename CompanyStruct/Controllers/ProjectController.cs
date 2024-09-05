using CompanyStruct.Models;
using CompanyStruct.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStruct.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        // GET: api/projects/all
        // Get all projects
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

        // GET: api/projects/{projectId}
        // Get project by ID
        [HttpGet("{projectId:int}")]
        public async Task<ActionResult<Division>> GetProjectById(int projectId)
        {
            var project = await _projectService.GetByIdAsync(projectId);

            if (project == null)
            {
                return NotFound("Project not found");
            }

            return Ok(project);
        }

        // POST: api/projects/add
        // Add new project
        [HttpPost("add")]
        public async Task<ActionResult<Project>> AddProject([FromBody] Project project)
        {
            var (isSuccess, errors) = await _projectService.AddAsync(project);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }
            return Ok("Project added");
        }

        // PUT: api/projects/update/{projectId}
        // Update project by ID
        [HttpPut("update/{projectId:int}")]
        public async Task<IActionResult> UpdateProject(int projectId, Project project)
        {
            var (isSuccess, errors) = await _projectService.UpdateAsync(projectId, project);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok("Project updated");
        }

        // DELETE: api/projects/delete/{projectId}
        // Delete project by ID
        [HttpDelete("delete/{projectId:int}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var (isSuccess, errors) = await _projectService.DeleteAsync(projectId);

            if (!isSuccess)
            {
                return BadRequest(new { Errors = errors });
            }

            return Ok("Project deleted");
        }
    }
}
