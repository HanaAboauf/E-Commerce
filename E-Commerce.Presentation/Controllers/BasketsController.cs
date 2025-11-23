using E_Commerce.Services.Exceptions;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.BasketDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController:ControllerBase
    {
        private readonly IBasketService _BasketService;
        public BasketsController(IBasketService basketService)
        {
            _BasketService = basketService;
        }
        [HttpGet]
        // GET BaseUrl/api/baskets?id={id}
        public async Task<ActionResult<BasketDTO?>> GetBasketAsync([FromQuery]string id)
        {
            var basket = await _BasketService.GetBasketAsync(id);
            if(basket is null ) throw new BasketNotFoundException(id);
            return Ok(basket);
        }
        [HttpPost]
        // POST: BaseUrl/api/baskets
        public async Task<ActionResult<BasketDTO?>> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            var createdOrUpdatedBasket = await _BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(createdOrUpdatedBasket);
        }
        [HttpDelete]
        [Route("{id}")]
        // Delete: BaseUrl/api/baskets/{id}
        public async Task<ActionResult> DeleteBasketAsync(string id)
        {
            var result = await _BasketService.DeleteBasketAsync(id);
            //if (!result) return NotFound();
            return NoContent();
        }
    }
}
