using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BusinessLogic.Profiles;
using BookStore.DataAccess.UnitOfWork;

namespace BookStore.Helpers
{
    public interface IMapperFactory
    {
        IMapper CreateMapper();
    }

    public class MapperFactory : IMapperFactory
    {
        private readonly IUnitOfWork _unitOfWork;
        public MapperFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IMapper CreateMapper()
        {
           var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AuthorProfile>();
                cfg.AddProfile<CategoryProfile>();
                cfg.AddProfile<WishProfile>();
                cfg.AddProfile<OrderProfile>();
                cfg.AddProfile<CartItemProfile>();
                cfg.AddProfile(new BookProfile(_unitOfWork));
                cfg.AddProfile(new CommentProfile(_unitOfWork));
            });
            return new Mapper(config);
        }
    }
}
