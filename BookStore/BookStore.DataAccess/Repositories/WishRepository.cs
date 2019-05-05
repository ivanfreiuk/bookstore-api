using BookStore.DataAccess.Context;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.Repositories
{
    public class WishRepository: BaseRepository<Wish>, IWishRepository
    {
        public WishRepository(StoreDbContext context) : base(context)
        {

        }
    }
}