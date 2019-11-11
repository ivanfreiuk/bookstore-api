using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess.Repositories.Interfaces
{
    public interface IWishRepository
    {
        Task<Wish> GetAsync(int userId, int bookId);

        Task<ICollection<Wish>> GetAllAsync();

        Task<ICollection<Wish>> GetWishesByUserIdAsync(int userId);

        Task AddAsync(Wish wish);

        Task UpdateAsync(Wish wish);

        Task RemoveAsync(Wish wish);
    }
}