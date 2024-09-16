using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_7._1
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int DeliveryId { get; set; }

        public Delivery Delivery { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
