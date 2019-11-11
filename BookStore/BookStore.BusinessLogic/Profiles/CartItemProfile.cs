using AutoMapper;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;

namespace BookStore.BusinessLogic.Profiles
{
    public class CartItemProfile: Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemDto>();
            CreateMap<CartItemDto, CartItem>();
        }
    }
}
