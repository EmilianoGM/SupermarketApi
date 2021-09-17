using SupermarketApi.Abstractions.Applications;
using SupermarketApi.Abstractions.Communication;
using SupermarketApi.Abstractions.Repositories;
using SupermarketApi.Application.Communication;
using SupermarketApi.Entities;
using SupermarketApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApi.Application
{
    public class ProductApplication : Application<Product>, IProductApplication
    {
        IProductRepository _productRespository;
        public ProductApplication(IProductRepository repository) : base(repository)
        {
            _productRespository = repository;
        }

        public async Task<IResponseWrapper<IEnumerable<Product>>> ListByCategoryAsync(int categoryId)
        {
            
            try
            {
                var entities = await _productRespository.ListByCategoryAsync(categoryId);
                return new ResponseWrapper<IEnumerable<Product>>(entities);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<Product>>("An error ocurred while retrieving the products.", ex.Message);
            }
        }

        public async Task<IResponseWrapper<IEnumerable<Product>>> ListByNameAsync(string name)
        {
            try
            {
                var entities = await _productRespository.ListByNameAsync(name);
                return new ResponseWrapper<IEnumerable<Product>>(entities);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<Product>>("An error ocurred while retrieving the products.", ex.Message);
            }
        }
    }
}
