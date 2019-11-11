using System;
using System.Threading.Tasks;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        ICommentRepository Comments { get; }
        ICategoryRepository Categories { get; }
        IWishRepository Wishes { get; }
        IOrderRepository Orders { get; }
        ICartItemRepository CartItems { get; }
        Task<int> SaveAsync();
    }
}
