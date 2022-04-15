using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementProductAPI.Models
{
    //1 supplier thì nhiều product
    public class Suppliers
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string Link { get; set; }
        public List<Products> Products { get; }
    }
}
