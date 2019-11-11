using AutoMapper;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;

namespace BookStore.BusinessLogic.Profiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
        }
    }
}
