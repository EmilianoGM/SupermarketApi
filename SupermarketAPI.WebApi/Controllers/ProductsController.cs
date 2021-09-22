using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupermarketApi.WebApi.Resources;
using SupermarketAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupermarketApi.Abstractions.Applications;
using SupermarketApi.Entities;
using SupermarketApi.Application.Communication;

namespace SupermarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductApplication _productApplication;
        private readonly IMapper _mapper;

        public ProductsController(IProductApplication productApplication, IMapper mapper)
        {
            _productApplication = productApplication;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var productsResponse = await _productApplication.ListAsync();
            if (productsResponse.Succeeded)
            {
                var productsResource = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(productsResponse.Data);
                return Ok(new ResponseWrapper<IEnumerable<ProductResource>>(productsResource));
            }
            return BadRequest(productsResponse);
        }

        [HttpGet]
        [Route("name/{name}")]
        public async Task<IActionResult> GetListByNameAsync(string name)
        {
            var productsResponse = await _productApplication.ListByNameAsync(name);
            if (productsResponse.Succeeded)
            {
                var productsResource = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(productsResponse.Data);
                return Ok(new ResponseWrapper<IEnumerable<ProductResource>>(productsResource));
            }
            return BadRequest(productsResponse);
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        public async Task<IActionResult> GetListByNameAsync(int categoryId)
        {
            var productsResponse = await _productApplication.ListByCategoryAsync(categoryId);
            if (productsResponse.Succeeded)
            {
                var productsResource = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(productsResponse.Data);
                return Ok(new ResponseWrapper<IEnumerable<ProductResource>>(productsResource));
            }
            return BadRequest(productsResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveProductResource resource)
        {
            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var response = await _productApplication.AddAsync(product);
            if (response.Succeeded)
            {
                var productResource = _mapper.Map<Product, ProductResource>(response.Data);
                return Ok(new ResponseWrapper<ProductResource>(productResource));
            }
            return BadRequest(response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProductResource resource)
        {
            Product product = _mapper.Map<SaveProductResource, Product>(resource);
            product.Id = id;
            var result = await _productApplication.UpdateAsync(product);
            if (result.Succeeded)
            {
                var productResource = _mapper.Map<Product, ProductResource>(result.Data);
                return Ok(new ResponseWrapper<ProductResource>(productResource));
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _productApplication.RemoveAsync(id);
            if (response.Succeeded)
            {
                var productResource = _mapper.Map<Product, ProductResource>(response.Data);
                return Ok(new ResponseWrapper<ProductResource>(productResource));
            }
            return BadRequest(response);
        }
    }
}
