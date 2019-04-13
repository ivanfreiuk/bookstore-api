using System;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.Repositories;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private IUserRepository _userRepository;
        private readonly StoreDbContext _context;
        private bool disposed;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            disposed = false;
        }

        public IUserRepository Users => _userRepository ?? (_userRepository = new UserRepository(_context));

        public int Save()
        {
            return _context.SaveChanges();
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
