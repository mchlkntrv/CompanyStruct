using CompanyStruct.Models;
using Microsoft.EntityFrameworkCore;
namespace CompanyStruct.Data
{
    public class CompanyDbContext(DbContextOptions<CompanyDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .ToTable("Employee")
                .HasOne<EmployeeType>()
                .WithMany()
                .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<EmployeeType>()
                .ToTable("EmployeeType");

            modelBuilder.Entity<Company>()
                .ToTable("Company")
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(c => c.Head);

            modelBuilder.Entity<Division>()
                .ToTable("Division")
                .HasOne<Division>()
                .WithMany()
                .HasForeignKey(d => d.CompanyId);

            modelBuilder.Entity<Division>()
                .ToTable("Division")
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(d => d.Head);

            modelBuilder.Entity<Project>()
                .ToTable("Project")
                .HasOne<Division>()
                .WithMany()
                .HasForeignKey(p => p.DivisionId);

            modelBuilder.Entity<Project>()
                .ToTable("Project")
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(p => p.Head);

            modelBuilder.Entity<Department>()
                .ToTable("Department")
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(dep => dep.ProjectId);

            modelBuilder.Entity<Department>()
                .ToTable("Department")
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(dep => dep.Head);
        }
    }
}
