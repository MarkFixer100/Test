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
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Order { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
       
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        

    

            

    


            modelBuilder.Entity<User>()
    .HasOne(u => u.Cart)
    .WithOne(c => c.User)
    .HasForeignKey<Cart>(c => c.Id) 
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
    .HasOne(o => o.User)
    .WithMany(u => u.Orders)
    .HasForeignKey(o => o.UserId)
    .OnDelete(DeleteBehavior.Cascade);

 

            modelBuilder.Entity<Cart>()
       .HasMany(o => o.Items)
       .WithOne(o => o.Cart)
       .HasForeignKey(o => o.CartId)
       ;
       

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
