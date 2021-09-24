using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using SupermarketApi.WebApi.Resources;
using SupermarketAPI.Extensions;
using SupermarketApi.Abstractions.Applications;
using SupermarketApi.Entities;
using SupermarketApi.Application.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SupermarketAPI.Controllers
{
    /*
     * All responses from API endpoints must return a resource.
     */
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController: ControllerBase
    {
        private readonly ICategoryApplication _categoryApplication;
        private readonly IMapper _mapper;

        public CategoriesController(IMapper mapper, ICategoryApplication categories)
        {
            _categoryApplication = categories;
            _mapper = mapper;
        }
    
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var categoriesResponse = await _categoryApplication.ListAsync();
            if (categoriesResponse.Succeeded)
            {
                var categoriesResource = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categoriesResponse.Data);
                return Ok(new ResponseWrapper<IEnumerable<CategoryResource>>(categoriesResource));
            } else
            {
                return BadRequest(categoriesResponse);
            }            
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _categoryApplication.FindByIdAsync(id);
            if(result.Succeeded)
            {
                var categoryResource = _mapper.Map<Category, CategoryResource>(result.Data);
                return Ok(new ResponseWrapper<CategoryResource>(categoryResource));
                
            }
            return BadRequest(result);
        }

        /*
        [HttpGet]
        public async Task<IActionResult> GetPagedAsync([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            PaginationFilter validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedCategories = await _categoryService.PagedListAsync(validFilter);
            var pagedResourceCategories = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(pagedCategories);
            int totalRecords = await _categoryService.CountAsync();
            double totalPages = Math.Ceiling(((double)totalRecords) / ((double)filter.PageSize));
            if(filter.PageNumber > totalPages)
            {
                return BadRequest("Invalid number of pages.");
            }
            PagedResponse<IEnumerable<CategoryResource>> pagedResponse = _uriService.CreatePagedResponse(pagedResourceCategories, filter.PageNumber, filter.PageSize, totalRecords, route);
            return Ok(pagedResponse);
        }
        */
        /*
         * Methods present in controller classes are called actions, and they have this signature because we can return
         * more than one possible result after the application executes the action.
         */
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource)
        {
            Category category = _mapper.Map<SaveCategoryResource, Category>(resource);
            var result = await _categoryApplication.AddAsync(category);
            if (result.Succeeded)
            {
                var categoryResource = _mapper.Map<Category, CategoryResource>(result.Data);
                return Ok(new ResponseWrapper<CategoryResource>(categoryResource));
            }
            return BadRequest(result);
        }

        /*
         * The ASP.NET Core pipeline parse the id fragment to the parameter of the same name.
        */

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource resource)
        {
            var category = _mapper.Map<SaveCategoryResource, Category>(resource);
            category.Id = id;
            var result = await _categoryApplication.UpdateAsync(category);
            if (result.Succeeded)
            {
                var categoryResource = _mapper.Map<Category, CategoryResource>(result.Data);
                return Ok(new ResponseWrapper<CategoryResource>(categoryResource));
            }
            return BadRequest(result);            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _categoryApplication.RemoveAsync(id);
            if (result.Succeeded)
            {
                var categoryResource = _mapper.Map<Category, CategoryResource>(result.Data);
                return Ok(categoryResource);
            }
            return BadRequest(result);
        }
    }
}
