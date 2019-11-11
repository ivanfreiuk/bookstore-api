using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess.Repositories.Interfaces
{
    public interface ICartItemRepository: IGenericRepository<CartItem>
    {
        Task<ICollection<CartItem>> GetCartItemsByUserId(int userId);
    }
}