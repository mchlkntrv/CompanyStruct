using Microsoft.EntityFrameworkCore;
namespace CompanyStruct.Models
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {
        }

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
                .HasOne(e => e.EmployeeTypeNavigation)
                .WithMany(et => et.Employees)
                .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<EmployeeType>().ToTable("EmployeeType");

            modelBuilder.Entity<Company>()
                .ToTable("Company")
                .HasOne(c => c.HeadNavigation)
                .WithMany()
                .HasForeignKey(c => c.Head);

            modelBuilder.Entity<Division>()
                .HasOne(d => d.CompanyNavigation)
                .WithMany(c => c.Divisions)
                .HasForeignKey(d => d.CompanyId);

            modelBuilder.Entity<Division>()
                .HasOne(d => d.HeadNavigation)
                .WithMany()
                .HasForeignKey(d => d.Head);

            modelBuilder.Entity<Project>()
                .ToTable("Project")
                .HasOne(p => p.DivisionNavigation)
                .WithMany(d => d.Projects)
                .HasForeignKey(p => p.DivisionId);

            modelBuilder.Entity<Project>()
                .ToTable("Project")
                .HasOne(p => p.HeadNavigation)
                .WithMany()
                .HasForeignKey(p => p.Head);

            modelBuilder.Entity<Department>()
                .HasOne(dep => dep.ProjectNavigation)
                .WithMany(p => p.Departments)
                .HasForeignKey(dep => dep.ProjectId);

            modelBuilder.Entity<Department>()
                .HasOne(dep => dep.HeadNavigation)
                .WithMany()
                .HasForeignKey(dep => dep.Head);
        }
    }
}
