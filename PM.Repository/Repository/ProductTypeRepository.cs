using PM.Common.Paging;
using PM.Data.Data;
using PM.Data.Models;
using PM.Repository.Repository.IRepository;
using System;
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

        public ICollection<ProductType> GetProductTypes(string sortBy, string searchString, int? pageNumber)
        {
            var allData = _app.ProductTypes.OrderBy(n => n.Name).ToList();
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
            allData = PaginatedList<ProductType>.Create(allData.AsQueryable(), pageNumber ?? 1, pageSize);
            return allData;
          
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
