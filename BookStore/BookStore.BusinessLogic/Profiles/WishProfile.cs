using AutoMapper;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;

namespace BookStore.BusinessLogic.Profiles
{
    public class WishProfile: Profile
    {
        public WishProfile()
        {
            CreateMap<Wish, WishDto>();
            CreateMap<WishDto, Wish>();
        }
    }
}