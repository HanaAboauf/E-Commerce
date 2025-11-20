using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }

        public Task<IEnumerable<string>> GetAllBrandsAsync()
        {
            var brands=_UnitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();

            return _Mapper.Map<Task<IEnumerable<string>>>(brands);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products=await _UnitOfWork.GetRepository<Product,int>().GetAllAsync();
            return _Mapper.Map<IEnumerable<ProductDTO>>(products);

        }

        public Task<IEnumerable<string>> GetAllTypesAsync()
        {
            var types=_UnitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            return _Mapper.Map<Task<IEnumerable<string>>>(types);
        }

        public Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            var product=_UnitOfWork.GetRepository<Product,int>().GetByIdAsync(id);
            return _Mapper.Map<Task<ProductDTO?>>(product);
        }
    }
}
