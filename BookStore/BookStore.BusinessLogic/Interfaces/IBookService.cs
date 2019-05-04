using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;

namespace BookStore.BusinessLogic.Interfaces
{
    public interface IBookService
    {
        Task<Book> GetBookAsync(int id);
        Task<ICollection<Book>> GetAllBooksAsync();
        Task<ICollection<BookShortDetail>> GetShortDetailBooksAsync();
        Task<ICollection<Book>> FindAsync(Expression<Func<Book, bool>> predicate);

        Task AddBookAsync(Book book);

        Task RemoveBookAsync(Book book);

        Task UpdateBookAsync(Book book);
    }
}