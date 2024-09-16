using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_7._1
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public int Count { get; set; }
        public string Producer { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
