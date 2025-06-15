using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace Infostructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Perfume> Perfumes { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           modelBuilder.Entity<Course>().HasKey(c => c.Id);

           modelBuilder.Entity<Student>().HasKey(s => s.Id);

            modelBuilder.ApplyConfiguration(new Config());

            modelBuilder.Entity<Course>()
    .HasMany(c => c.Students)
    .WithOne(s => s.Course)     
    .HasForeignKey(s => s.CourseId);


            var category1Id = new Guid("11111111-1111-1111-1111-111111111111");
            var category2Id = new Guid("22222222-2222-2222-2222-222222222222");

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = category1Id, Name = "Категория 1" },
                new Category { Id = category2Id, Name = "Категория 2" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Name = "Продукт 1",
                    Type = "Тип 1",
                    PricePerKg = 100,
                    Weight = 1.5,
                    ProductionDate = new DateTime(2024, 6, 1),
                    ExpirationDate = new DateTime(2025, 6, 1),
                    StorageCondition = "Хранить в прохладном месте",
                    IsAvailable = true,
                    CategoryId = category1Id
                }
            );


        }

    }
}
