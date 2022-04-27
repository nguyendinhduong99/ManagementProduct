using PM.Data.Data;
using PM.Data.Models;
using PM.Repository.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace PM.Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _app;
        public ProductRepository(AppDbContext app)
        {
            _app = app;
        }
        public bool CreateProduct(Product product)
        {
            _app.Products.Add(product);
            return Save();
        }

        public bool DeleteProduct(Product product)
        {
            _app.Products.Remove(product);
            return Save();
        }

        public Product GetProductById(int productId)
        {
            return _app.Products.FirstOrDefault(n => n.Id == productId);
        }

        public ICollection<Product> GetProducts()
        {
            return _app.Products.ToList();
        }

        public bool ProductExist(string name)
        {
            var result = _app.Products.Any(n => n.Name.ToLower().Trim() == name.ToLower().Trim());
            return result;
        }

        public bool ProductExist(int id)
        {
            var result = _app.Products.Any(n => n.Id == id);
            return result;
        }

        public bool Save()
        {
            return _app.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateProduct(Product product)
        {
            _app.Products.Update(product);
            return Save();
        }
    }
}
