using PM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Repository.Repository.IRepository
{
    public interface IProductRepository
    {
        //Crud
        ICollection<Product> GetProducts(string sortBy, string searchString, int? pageNumber);

        Product GetProductById(int productId);
        bool ProductExist(string name);
        bool ProductExist(int id);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool Save();
    }
}
