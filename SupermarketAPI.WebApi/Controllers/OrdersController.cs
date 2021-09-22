using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupermarketApi.Abstractions.Applications;
using SupermarketApi.WebApi.Resources;
using SupermarketApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupermarketApi.Application.Communication;

namespace SupermarketApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderApplication _orderApplication;
        private readonly IMapper _mapper;

        public OrdersController(IOrderApplication orderApplication, IMapper mapper)
        {
            _orderApplication = orderApplication;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var orderResponse = await _orderApplication.ListAsync();
            if (orderResponse.Succeeded)
            {
                var orderResource = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orderResponse.Data);
                return Ok(new ResponseWrapper<IEnumerable<OrderResource>>(orderResource));
            }
            else
            {
                return BadRequest(orderResponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveOrderResource resource)
        {
            Order order = _mapper.Map<SaveOrderResource, Order>(resource);
            var response = await _orderApplication.AddWithProductsAsync(order, resource.ProductsId);
            if (response.Succeeded)
            {
                var orderResource = _mapper.Map<Order, OrderResource>(response.Data);
                return Ok(new ResponseWrapper<OrderResource>(orderResource));
            }
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _orderApplication.RemoveAsync(id);
            if (response.Succeeded)
            {
                var orderResource = _mapper.Map<Order, OrderResource>(response.Data);
                return Ok(new ResponseWrapper<OrderResource>(orderResource));
            }
            return BadRequest(response);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOrderResource resource)
        {
            Order order = _mapper.Map<SaveOrderResource, Order>(resource);
            order.OrderId = id;
            var response = await _orderApplication.UpdateWithProductsAsync(order, resource.ProductsId);
            if (response.Succeeded)
            {
                var orderResource = _mapper.Map<Order, OrderResource>(response.Data);
                return Ok(new ResponseWrapper<OrderResource>(orderResource));
            }
            return BadRequest(response);
        }

    }
}
