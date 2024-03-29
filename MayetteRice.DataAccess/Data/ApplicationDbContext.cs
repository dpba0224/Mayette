using MayetteRice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MayetteRice.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This line is used because keys of identity tables are mapped in the OnModelCreating and to avoid errors
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Rice", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sugar", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Flour", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Grains", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Salt", DisplayOrder = 5 },
                new Category { Id = 6, Name = "Cooking Oil", DisplayOrder = 6 },
                new Category { Id = 7, Name = "Baking Goods", DisplayOrder = 7 },
                new Category { Id = 8, Name = "Other", DisplayOrder = 8 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product 
                {
                    Id = 1,
                    Title = "Angelica (1kg)",
                    Description = "A delicious rice that can fit to your family's needs!",
                    Price = 59.00,
                    DiscPrice = 58.00,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "Sinandomeng (1kg)",
                    Description = "A delicious rice that can fit to your family's needs!",
                    Price = 54.00,
                    DiscPrice = 53.00,
                    CategoryId = 1,
                    ImageUrl = ""
                }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company 
                { 
                    Id = 1,
                    Name = "Coca-Cola Beverages Philippines Inc.",
                    StreetAddress = "Santa Rosa Exit",
                    Barangay = "Pulong Santa Cruz",
                    City = "Santa Rosa",
                    Province = "Laguna",
                    PostalCode = "4026",
                    PhoneNumber = "+639061234567"
                },
                new Company
                {
                    Id = 2,
                    Name = "Amherst Laboratories Inc.",
                    StreetAddress = "Unilab PharmaCampus",
                    Barangay = "Mamplasan",
                    City = "Binan",
                    Province = "Laguna",
                    PostalCode = "4026",
                    PhoneNumber = "+639067654321"
                },
                new Company
                {
                    Id = 3,
                    Name = "Santa Rosa City Hall",
                    StreetAddress = "J.P. Rizal Street",
                    Barangay = "Malusak",
                    City = "Santa Rosa",
                    Province = "Laguna",
                    PostalCode = "4026",
                    PhoneNumber = "+639977654321"
                }
            );
        }
    }
}
