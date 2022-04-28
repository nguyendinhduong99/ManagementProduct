using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Data.Models.Dtos
{
    public class ProductDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descripton { get; set; }
        
        public long BuyPrice { set; get; }
        public long SalePrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public string ProductImg { get; set; }
        public DateTime DateCreated { set; get; }
        public int ProductTypeId { get; set; }
        public List<ProductType> ProductType { get; set; }
    }
}
