using Microsoft.EntityFrameworkCore;
using WorkPlanner.Entities;
using Task = WorkPlanner.Entities.Task;

namespace WorkPlanner.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja encji Task jeśli potrzebna
            modelBuilder.Entity<Task>().ToTable("Tasks");
        }
    }
}