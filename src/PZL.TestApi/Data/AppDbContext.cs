using Microsoft.EntityFrameworkCore;
using PZL.TestApi.Domain;

namespace PZL.TestApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects => Set<Project>();
        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasMany(project => project.Tasks)
                .WithOne()
                .HasForeignKey(task => task.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskItem>()
                .HasMany(task => task.Comments)
                .WithOne()
                .HasForeignKey(comment => comment.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

