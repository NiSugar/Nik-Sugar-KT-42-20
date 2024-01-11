using Microsoft.EntityFrameworkCore;
using NiSugarKT_42_20.Models;
using NiSugarKT_42_20.Database.Configurations;

namespace NiSugarKT_42_20.Database
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Classes> Classes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new ClassesConfiguration());
        }

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
    }
}
