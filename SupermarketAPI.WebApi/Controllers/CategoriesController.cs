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

namespace SupermarketAPI.Controllers
{
    /*
     * All responses from API endpoints must return a resource.
     */
    [Route("api/[controller]")]
    public class CategoriesController: Controller
    {
        private readonly ICategoryApplication _categories;
        private readonly IMapper _mapper;

        public CategoriesController(IMapper mapper, ICategoryApplication categories)
        {
            _categories = categories;
            _mapper = mapper;
        }

        /*
         *  Map the enumeration of categories to an enumeration of resources.
         */
        /*
        [HttpGet]
        public async Task<IEnumerable<CategoryResource>> GetAllAsync()
        {
            var categories = await _categoryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);

            return resources;
        }*/

        
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var categoriesResponse = await _categories.ListAsync();
            if (categoriesResponse.Succeeded)
            {
                var categoriesResource = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categoriesResponse.Data);
                return Ok(new ResponseWrapper<IEnumerable<CategoryResource>>(categoriesResource));
            } else
            {
                return BadRequest(categoriesResponse);
            }
            /*
            var categories = await _categoryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
            return Ok(new PagedResponse<IEnumerable<CategoryResource>>(resources));*/
        }

        /*
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _categoryService.FindByIdAsync(id);
            if(!result.Success)
            {
                return BadRequest(result.Message);
            }
            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Data);
            return Ok(categoryResource);
        }*/

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
        /*
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
            var category = _mapper.Map<SaveCategoryResource, Category>(resource);
            var result = await _categoryService.SaveAsync(category);
            if (!result.Success) return BadRequest(result.Message);
            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Data);
            return Ok(categoryResource);
        }*/

        /*
         * The ASP.NET Core pipeline parse the id fragment to the parameter of the same name.
         */

        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource resource)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
            var category = _mapper.Map<SaveCategoryResource, Category>(resource);
            var result = await _categoryService.UpdateAsync(id, category);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Data);
            return Ok(categoryResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            if (!result.Success) return BadRequest(result.Message);
            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Data);
            return Ok(categoryResource);
        }*/
    }
}
