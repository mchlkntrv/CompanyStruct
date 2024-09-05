using CompanyStruct.Data;
using CompanyStruct.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyStruct.Repositories
{
    public class ProjectRepository(CompanyDbContext context) : IProjectRepository
    {
        private readonly CompanyDbContext _context = context;

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int projectId)
        {
            return await _context.Projects.FindAsync(projectId);
        }

        public async Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUsedAsync(int projectId)
        {
            return await _context.Departments.AnyAsync(c => c.ProjectId == projectId);
        }
    }
}
