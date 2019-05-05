using System.Linq;
using System.Transactions;
using AutoMapper;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.UnitOfWork;

namespace BookStore.BusinessLogic.Profiles
{
    public class BookProfile: Profile 
    {
        public BookProfile(IUnitOfWork uow)
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Mark,
                    opt => opt.MapFrom(b => uow.Marks.GetMarkByBook(b.Id)))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(b => b.BookCategories.Select(i => i.Category)))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(b => b.BookAuthors.Select(i => i.Author)));
        }
    }
}