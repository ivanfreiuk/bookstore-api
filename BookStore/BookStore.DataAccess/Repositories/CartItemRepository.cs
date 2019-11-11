using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class CartItemRepository: BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(StoreDbContext context) : base(context)
        {
        }

        public override async Task<CartItem> GetAsync(int id)
        {
            return await _context.CartItems
                .Include(ci => ci.Book)
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }


        public async Task<ICollection<CartItem>> GetCartItemsByUserId(int userId)
        {
            return await _context.CartItems
                .Include(ci => ci.Book)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();
        }

        public override async Task UpdateAsync(CartItem cartItem)
        {
            var cartItemFromDb = await _context.CartItems
                .FirstOrDefaultAsync(i => i.Id == cartItem.Id);

            cartItemFromDb.IsOrdered = cartItem.IsOrdered;
            cartItemFromDb.Quantity = cartItem.Quantity;

            _context.CartItems.Update(cartItemFromDb);
        }
    }
}