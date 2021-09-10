﻿using SupermarketAPI.Domain.Models;
using SupermarketAPI.Domain.Repositories;
using SupermarketAPI.Domain.Services;
using SupermarketAPI.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _productRepository.ListAsync();
        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try
            {
                var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
                if(existingCategory == null)
                {
                    return new ProductResponse("Invalid category.");
                }
                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred while saving the product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateAsync(int id, Product product)
        {
            try
            {
                var existingProduct = await _productRepository.FindByIdAsync(id);
                if (existingProduct == null)
                {
                    return new ProductResponse("Product not found.");
                }
                var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
                if (existingCategory == null)
                {
                    return new ProductResponse("The category is invalid.");
                }
                existingProduct.Name = product.Name;
                existingProduct.QuantityInPackage = product.QuantityInPackage;
                existingProduct.UnitOfMeasurement = product.UnitOfMeasurement;
                existingProduct.CategoryId = product.CategoryId;
                _productRepository.Update(existingProduct);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred while updating the product: {ex.Message}");
            }
           

        }

        public async Task<ProductResponse> DeleteAsync(int id)
        {
            try
            {
                var existingProduct = await _productRepository.FindByIdAsync(id);
                if(existingProduct == null)
                {
                    return new ProductResponse("Invalid product id.");
                }
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred while deleting the product: {ex.Message}");
            }
        }
    }
}
