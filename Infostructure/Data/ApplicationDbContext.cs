using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace Infostructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        
        }
        public DbSet<Perfume> Perfumes { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Course>().HasKey(c => c.Id);

           modelBuilder.Entity<Student>().HasKey(s => s.Id);

            modelBuilder.ApplyConfiguration(new Config());

            modelBuilder.Entity<Course>()
    .HasMany(c => c.Students)
    .WithOne(s => s.Course)     
    .HasForeignKey(s => s.CourseId);
  

        }

    }
}
