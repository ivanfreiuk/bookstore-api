using System;
using System.Threading.Tasks;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.Repositories;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private IUserRepository _userRepository;
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;
        private ICategoryRepository _categoryRepository;
        private ICommentRepository _commentRepository;
        private IWishRepository _wishRepository;
        private IOrderRepository _orderRepository;
        private ICartItemRepository _cartItemRepository;

        private readonly StoreDbContext _context;
        private bool disposed;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            disposed = false;
        }

        public IUserRepository Users => _userRepository ?? (_userRepository = new UserRepository(_context));

        public IBookRepository Books => _bookRepository ?? (_bookRepository = new BookRepository(_context));
        
        public IAuthorRepository Authors => _authorRepository ?? (_authorRepository = new AuthorRepository(_context));

        public ICommentRepository Comments => _commentRepository ?? (_commentRepository = new CommentRepository(_context));

        public ICategoryRepository Categories => _categoryRepository ?? (_categoryRepository = new CategoryRepository(_context));

        public IWishRepository Wishes => _wishRepository ?? (_wishRepository = new WishRepository(_context));

        public IOrderRepository Orders => _orderRepository ?? (_orderRepository = new OrderRepository(_context));

        public ICartItemRepository CartItems => _cartItemRepository ?? (_cartItemRepository = new CartItemRepository(_context));

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }


        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
