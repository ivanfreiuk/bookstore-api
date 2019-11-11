using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Models;

namespace BookStore.BusinessLogic.Interfaces
{
    public interface ICartItemService
    {
        Task<CartItemDto> GetCartItemAsync(int id);

        Task<ICollection<CartItemDto>> GetAllCartItemsAsync();

        Task<ICollection<CartItemDto>> GetCartItemsByUserId(int userId);

        Task AddCartItemAsync(CartItemDto cartItem);

        Task RemoveCartItemAsync(int id);

        Task UpdateCartItemAsync(CartItemDto cartItem);
    }
}