using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketModule;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _BasketRepository;
        private readonly IMapper _Mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper) 
        {
            _BasketRepository = basketRepository;
            _Mapper = mapper;
        }
        public async Task<BasketDTO?> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            var customerBasket = _Mapper.Map<CustomerBasket>(basket);
             var CreatedOrUpdatedBasket=await _BasketRepository.CreateOrUpdateBasketAsync(customerBasket);
            return _Mapper.Map<BasketDTO>(CreatedOrUpdatedBasket);

        }

        public async Task<bool> DeleteBasketAsync(string basketId)=> await _BasketRepository.DeleteBasketAsync(basketId);


        public async Task<BasketDTO?> GetBasketAsync(string basketId)
        {
            var basket = await _BasketRepository.GetBasketAsync(basketId);
            if (basket == null) return null;
            return _Mapper.Map<CustomerBasket, BasketDTO>(basket);
        }
    }
}
