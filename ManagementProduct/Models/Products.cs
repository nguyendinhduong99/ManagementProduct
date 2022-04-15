using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementProductAPI.Models
{
    //nhiều product thì có 1 supplier, 1 category
    //1 product thi nhieu orderdetail
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Descripton { get; set; }
        public int SupplierId { get; set; }
        public Suppliers Suppliers { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal BuyPrice { set; get; }
        [Required]
        public decimal SalePrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public string PathImg { get; set; }
        public DateTime DateCreated { set; get; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
