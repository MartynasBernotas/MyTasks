using Microsoft.EntityFrameworkCore;
using MyTasks.Models;

namespace MyTasks.Database
{
    public partial class DatabaseContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DatabaseConnection"));
        }

        public virtual DbSet<MyTask> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyTask>(entity =>
            {
                modelBuilder.Entity<MyTask>()
                .HasOne(p => p.ParentTask)
                .WithMany(b => b.SubTasks)
                .HasForeignKey(p => p.ParentTaskId)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<MyTask>()
                .ToTable("Task")
                .Property(e => e.Id)
                .HasColumnName("Id");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
