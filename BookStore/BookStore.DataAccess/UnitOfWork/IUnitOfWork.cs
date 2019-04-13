using System;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        int Save();
    }
}
