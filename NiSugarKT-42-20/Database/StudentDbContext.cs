using Microsoft.EntityFrameworkCore;
using NiSugarKT_42_20.Models;
using NiSugarKT_42_20.Database.Configurations;

namespace NiSugarKT_42_20.Database
{
    public class StudentDbContext : DbContext
    {
        DbSet<Student> Students { get; set; }
        DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
        }

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
    }
}
