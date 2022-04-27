using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Data.Models.Dtos
{
    public class CreateProductTypeDto
    {
        public string Name { get; set; }
        public string Descripton { get; set; }
        public bool Active { get; set; }
        public List<Product> Product { get; set; }
    }
}
