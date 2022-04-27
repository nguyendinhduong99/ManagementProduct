using ParkyAPI.Models;
using PM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Repository.Repository.IRepository
{
    public interface IProductTypeRepository
    {
        //Crud
        ICollection<ProductType> GetProductTypes();

        ProductType GetProductTypeById(int productTypeId);
        bool ProductTypeExist(string name);
        bool ProductTypeExist(int id);
        bool CreateProductType(ProductType productType);
        bool UpdateProductType(ProductType productType);
        bool DeleteProductType(ProductType productType);
        bool Save();
    }
}
