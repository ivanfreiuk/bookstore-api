using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;

namespace BookStore.BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderAsync(int id);

        Task<ICollection<OrderDto>> GetAllOrdersAsync();

        Task AddOrderAsync(OrderDto order);

        Task RemoveOrderAsync(int id);

        Task UpdateOrderAsync(OrderDto order);
    }
}