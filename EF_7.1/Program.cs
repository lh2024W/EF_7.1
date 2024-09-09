using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EF_7._1
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public int Count { get; set; }
        public string Producer { get; set; }
        public int ExpirationDate { get; set; }
        public int CategoryId { get; set; }
        public string DeliveryId { get; set; }
        public Category Category { get; set; }
        public Delivery Delivery { get; set; }

    }

    public class Delivery
    {
        public int Id { get; set; }
        public string LongName { get; set; }
        public string Address { get; set; }
        public string PaymentType { get; set; }

        public string Status { get; set; }
        public string BankDiteils { get; set; }

        public DateTime DateOfDispatch { get; set; }
        public DateTime DateOfReceipt { get; set; }

        public List<Product> Products { get; set; }
        public List<User> Users { get; set; }

    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Delivery> Delivery { get; set; }
        public List<Product> Products { get; set; }
    }



    public class Program
    {
        static void Main()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                Product products = new Product
                {
                    //Name = "Морковь",

                };

            }
        }


        public class ApplicationContext : DbContext
        {
            public DbSet<Product> Products { get; set; } = null!;
            public DbSet<Delivery> Delivery { get; set; } = null!;
            public DbSet<User> Users { get; set; } = null!;


            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Server=WIN-UKQRC56FDU3;Database=testDb;Trusted_Connection=True;TrustServerCertificate=True;");

            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>().HasMany(e => e.Delivery).WithMany(e => e.Products);
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
            
            
            
    

