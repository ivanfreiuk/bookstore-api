using AutoMapper;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.UnitOfWork;

namespace BookStore.BusinessLogic.Profiles
{
    public class CommentProfile: Profile
    {
        public CommentProfile(IUnitOfWork uow)
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(c=>c.User.UserName));
            CreateMap<CommentDto, Comment>();
        }
    }
}