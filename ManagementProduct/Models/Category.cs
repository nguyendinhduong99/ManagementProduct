using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementProductAPI.Models
{
    //1 category thif nhieeuf product
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public string Picture { get; set; }
        public bool Active { get; set; }
        public List<Products>  Products { get; set; }
    }
}
