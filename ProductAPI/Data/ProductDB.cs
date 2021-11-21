using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductAPI.Model;
using ProductAPI.Service;
using Microsoft.Extensions.Logging;

namespace ProductAPI.Data
{
    public class ProductDB : IProductRepo
    {
        public readonly ILogger<ProductDB> _logger;
        public DataBaseContext _dbContext;

        public ProductDB(ILogger<ProductDB> logger, DataBaseContext dbcontext)
        {
            _logger = logger;
            _dbContext = dbcontext;
        }

        public bool AddProduct(Product product)
        {            
            try
            {
                if (product == null)
                {
                    _logger.LogWarning("Exception on adding the product : Product data cannot be null");
                    throw new ArgumentNullException();
                }

                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on adding the product : ${ex.Message}");                
                return false;
            }
        }

        public bool DeleteProduct(Product product)
        {
            try
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on deleting the product : {ex.Message}");
                return false;                
            }                       
            
        }

        public Product GetProductById(int id)
        {
            try
            {
                return _dbContext.Products.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on retreving the product by ID : {ex.Message}");
                return null;                
            }

        }

        public IEnumerable<Product> GetProductList()
        {
            try
            {
                return _dbContext.Products.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on getting the list of product : {ex.Message}");
                return null;
            }
                                    
        }

        public bool UpdateProduct(Product obj)
        {
            try
            {           
                _dbContext.Products.Update(obj);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on updating the product : {ex.Message}");
                return false;
            }                
        }
    }
}
