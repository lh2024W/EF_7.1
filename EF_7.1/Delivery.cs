using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_7._1
{
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

        public ICollection<Order> Orders { get; set; }
    }
}
