using ProductAPI.Model;
using System.Collections.Generic;

namespace ProductAPI.Service
{
    public interface IProductRepo
    {
        IEnumerable<Product> GetProductList();
        Product GetProductById(int id);
        bool AddProduct(Product obj);
        bool UpdateProduct(Product obj);
        bool DeleteProduct(Product id);

    }
}
