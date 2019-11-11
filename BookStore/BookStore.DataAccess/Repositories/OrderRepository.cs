using BookStore.DataAccess.Context;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.Repositories
{
    public class OrderRepository: BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(StoreDbContext context) : base(context)
        {
        }
    }
}