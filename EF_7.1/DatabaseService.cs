using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_7._1
{
    public class DatabaseService
    {
        DbContextOptions<ApplicationContext> options;

        public void EnsurePopulated()
        {

            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                List<User> users = new List<User>
                {
                    new User { Email = "john@gmail.com", Password = "111" },
                    new User { Email = "alice@gmail.com", Password = "222" },
                    new User { Email = "bob@gmail.com", Password = "333" }
                };
                db.Users.AddRange(users);

                List<Category> categories = new List<Category>
                {
                    new Category { Name = "Духи" },
                    new Category { Name = "Туалетная вода" },
                    new Category { Name = "Парфюмированная вода" }
                };
                db.Categories.AddRange(categories);

                List<Product> products = new List<Product>
                {
                    new Product
                    {
                        Name = "Allure",
                        PurchasePrice = 4000.00m,
                        RetailPrice = 6525.00m,
                        Count = 10,
                        Producer = "CHANEL",
                        ExpirationDate = new DateTime(2024, 10, 12),
                        CategoryId = 1
                    },
                    new Product
                    {
                        Name = "Miss Dior",
                        PurchasePrice = 2500,
                        RetailPrice = 3642.00m,
                        Count = 12,
                        Producer = "DIOR",
                        ExpirationDate = new DateTime(2024, 10, 12),
                        CategoryId = 2
                    }
                };
                db.Products.AddRange(products);

                List<Delivery> deliveries = new List<Delivery>
                {
                    new Delivery { LongName = "Delivery1", Address = "Test1", PaymentType = "card", Status = "оплачено",
                        BankDiteils = "р/с 256444666 иркавуу", DateOfDispatch = new DateTime(2024, 02, 02),
                        DateOfReceipt = new DateTime(2024, 02, 02) },
                    new Delivery { LongName = "Delivery2", Address = "Test2", PaymentType = "card", Status = "не оплачено",
                        BankDiteils = "р/с 111111111111 дтаалвв", DateOfDispatch = new DateTime(2024, 03, 03),
                        DateOfReceipt = new DateTime(2024, 03, 03) }
                };
                db.Deliveries.AddRange(deliveries);

                db.Orders.AddRange(
                    new Order
                    {
                        User = users[0],
                        Date = DateTime.Now.AddDays(-1),
                        DeliveryId = 1,
                        OrderDetails = new OrderDetail[]
                        {
                            new OrderDetail
                            {
                                Quantity = 1,
                                Product = products[0]
                            },
                            new OrderDetail
                            {
                                Quantity = 1,
                                Product = products[1]
                            }
                        }
                    },
                    new Order
                    {
                        User = users[1],
                        Date = DateTime.Now.AddDays(-2),
                        DeliveryId = 2,
                        OrderDetails = new OrderDetail[]
                        {
                            new OrderDetail
                            {
                                Quantity = 1,
                                Product = products[1]
                            }
                        }
                    }
                );
                db.SaveChanges();

            }

        }

        public void PrintAllOrders()
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var query = db.Orders.Select(order => new
                {
                    Name = order.Delivery.LongName,
                    Email = order.User.Email,
                    Address = order.Delivery.Address,
                    TotalOrders = order.Delivery.Orders.Count()

                }).ToList();

                foreach (var item in query)
                {
                    Console.WriteLine($"Name: {item.Name}");
                    Console.WriteLine($"Email: {item.Email}");
                    Console.WriteLine($"Address: {item.Address}");
                    Console.WriteLine($"Total Orders: {item.TotalOrders}");

                    Console.WriteLine();

                }
            }
        }

        public void RemoveOrderById( int idOrder)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var removeOrderById = db.Orders.FirstOrDefault(e => e.Id == idOrder);
                if (removeOrderById != null)
                {
                    db.Orders.Remove(removeOrderById);
                    db.SaveChanges();
                }
            }
        }
    }
}
