using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class WishRepository: BaseRepository<Wish>, IWishRepository
    {
        public WishRepository(StoreDbContext context) : base(context)
        {

        }

        public async Task<Wish> GetAsync(int userId, int bookId)
        {
            return await _context.Wishes
                .Include(w => w.Book)
                .Include(w => w.User)
                .Where(w => w.UserId == userId && w.BookId == bookId)
                .FirstAsync();
        }
        public async Task<ICollection<Wish>> GetWishesByUserIdAsync(int userId)
        {
            return await _context.Wishes
                .Include(w => w.Book)
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }
    }
}