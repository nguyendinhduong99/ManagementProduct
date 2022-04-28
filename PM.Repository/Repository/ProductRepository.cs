using PM.Common.Paging;
using PM.Data.Data;
using PM.Data.Models;
using PM.Repository.Repository.IRepository;
using System;
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

        public ICollection<Product> GetProducts(string sortBy, string searchString, int? pageNumber)
        {
            var allData = _app.Products.OrderBy(n => n.Name).ToList();
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "desc":
                        allData = allData.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                allData = allData.Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            //Paging
            int pageSize = 5;
            allData = PaginatedList<Product>.Create(allData.AsQueryable(), pageNumber ?? 1, pageSize);
            return allData;
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
