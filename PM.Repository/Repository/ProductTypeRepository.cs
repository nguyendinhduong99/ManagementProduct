using PM.Data.Data;
using PM.Data.Models;
using PM.Repository.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace PM.Repository.Repository
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly AppDbContext _app;
        public ProductTypeRepository(AppDbContext app)
        {
            _app = app;
        }
        public bool CreateProductType(ProductType productType)
        {
            _app.ProductTypes.Add(productType);
            return Save();
        }

        public bool DeleteProductType(ProductType productType)
        {
            _app.ProductTypes.Remove(productType);
            return Save();
        }

        public ProductType GetProductTypeById(int productTypeId)
        {
            return _app.ProductTypes.FirstOrDefault(n => n.Id == productTypeId);
        }

        public ICollection<ProductType> GetProductTypes()
        {
            return _app.ProductTypes.ToList();
        }

        public bool ProductTypeExist(string name)
        {
            var result = _app.ProductTypes.Any(n => n.Name.ToLower().Trim() == name.ToLower().Trim());
            return result;
        }

        public bool ProductTypeExist(int id)
        {
            var result = _app.ProductTypes.Any(n => n.Id == id);
            return result;
        }

        public bool Save()
        {
            return _app.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateProductType(ProductType productType)
        {
            _app.ProductTypes.Update(productType);
            return Save();
        }
    }
}
