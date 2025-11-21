using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductDTOs;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductsController(IProductService productService)
        {
            _ProductService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>>GetAllProductsAsync([FromQuery]ProductQueryParams queryParams)
        {
            var products = await _ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int id)
        {
            var product = await _ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet]
        [Route("brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllBrandsAsync()
        {
            var brands = await _ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
        [HttpGet]
        [Route("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllTypesAsync()
        {
            var types = await _ProductService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}
