using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.UnitOfWork;

namespace BookStore.BusinessLogic.Services
{
    public class OrderService: BaseService, IOrderService
    {
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            var orderEntity = await _uow.Orders.GetAsync(id);

            var orderDto = _mapper.Map<Order, OrderDto>(orderEntity);

            return orderDto;
        }

        public async Task<ICollection<OrderDto>> GetAllOrdersAsync()
        {
            var orderEntities = await _uow.Orders.GetAllAsync();

            var orderDtos = _mapper.Map<IEnumerable<Order>, ICollection<OrderDto>>(orderEntities);

            return orderDtos;
        }

        public async Task AddOrderAsync(OrderDto order)
        {
            var orderEntity = _mapper.Map<OrderDto, Order>(order);

            await _uow.Orders.AddAsync(orderEntity);

            await _uow.SaveAsync();
        }

        public async Task RemoveOrderAsync(int id)
        {
            var orderEntity = await _uow.Orders.GetAsync(id);

            await _uow.Orders.RemoveAsync(orderEntity);

            await _uow.SaveAsync();
        }

        public async Task UpdateOrderAsync(OrderDto order)
        {
            var orderEntity = _mapper.Map<OrderDto, Order>(order);

            await _uow.Orders.AddAsync(orderEntity);

            await _uow.SaveAsync();
        }
    }
}
