using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementProductAPI.Models
{
    public class Payments
    {
        public int Id { get; set; }
        public string PaymentType { get; set; }
        public string Allowed { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
