using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.UnitOfWork;

namespace BookStore.BusinessLogic.Services
{
    public class WishService: BaseService, IWishService
    {
        private readonly IMapper _mapper;

        public WishService(IUnitOfWork uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
        }

        public async Task<WishDto> GetWishAsync(int userId, int bookId)
        {
            var wishEntity = await _uow.Wishes.GetAsync(userId, bookId);

            var wishDto = _mapper.Map<Wish, WishDto>(wishEntity);

            return wishDto;
        }

        public async Task<ICollection<WishDto>> GetAllWishesAsync()
        {
            var wishEntities = await _uow.Wishes.GetAllAsync();

            var wishDtos = _mapper.Map<IEnumerable<Wish>, ICollection<WishDto>>(wishEntities);

            return wishDtos;
        }

        public async Task<ICollection<WishDto>> GetWishesByUserIdAsync(int userId)
        {
            var wishEntities = await _uow.Wishes.GetAllAsync();

            var wishDtos = _mapper.Map<IEnumerable<Wish>, ICollection<WishDto>>(wishEntities);

            return wishDtos;
        }

        public async Task AddWishAsync(WishDto wish)
        {
            var wishEntity = _mapper.Map<WishDto, Wish>(wish);

            await _uow.Wishes.AddAsync(wishEntity);

            await _uow.SaveAsync();
        }

        public async Task AddUpdateAsync(WishDto wish)
        {
            var wishEntity = _mapper.Map<WishDto, Wish>(wish);

            await _uow.Wishes.AddAsync(wishEntity);

            await _uow.SaveAsync();
        }

        public async Task RemoveWishAsync(int userId, int bookId)
        {
            var wishEntity = await _uow.Wishes.GetAsync(userId, bookId);

            await _uow.Wishes.RemoveAsync(wishEntity);

            await _uow.SaveAsync();
        }
    }
}