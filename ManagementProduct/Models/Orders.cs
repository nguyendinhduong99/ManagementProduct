using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementProductAPI.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customers Customers { get; set; }
        public int PaymentsId { get; set; }
        public Payments Payments { get; set; }
        public int ShipperId { get; set; }
        public Shippers Shippers { get; set; }
        public DateTime OrderDate { set; get; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
