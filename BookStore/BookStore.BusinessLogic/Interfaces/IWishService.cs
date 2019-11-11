using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Models;

namespace BookStore.BusinessLogic.Interfaces
{
    public interface IWishService
    {
        Task<WishDto> GetWishAsync(int userId, int bookId);

        Task<ICollection<WishDto>> GetAllWishesAsync();

        Task<ICollection<WishDto>> GetWishesByUserIdAsync(int userId);

        Task AddWishAsync(WishDto wish);

        Task RemoveWishAsync(int userId, int bookId);
    }
}