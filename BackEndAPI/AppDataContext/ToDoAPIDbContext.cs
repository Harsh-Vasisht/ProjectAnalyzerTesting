using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using BackEndAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BackEndAPI.AppDataContext
{
    public class ToDoAPIDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly DbSettings _dbSettings;
        public DbSet<ToDo> toDos { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public ToDoAPIDbContext(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ToDo>()
                            .HasOne(t => t.User)
                            .WithMany()
                            .HasForeignKey(t => t.UserId);
            modelBuilder.Entity<ToDo>()
                            .HasOne(t => t.Goal)
                            .WithMany()
                            .HasForeignKey(t => t.GoalId)
                            .IsRequired(false);
            modelBuilder.Entity<Goal>()
                            .HasOne(t => t.User)
                            .WithMany()
                            .HasForeignKey(t => t.UserId);
        }
    }
}