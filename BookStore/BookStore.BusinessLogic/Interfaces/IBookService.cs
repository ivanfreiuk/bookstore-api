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
        Task<BookDto> GetBookAsync(int id);

        Task<ICollection<BookDto>> GetAllBooksAsync();

        Task<ICollection<BookShortDetail>> GetShortDetailBooksAsync();
        
        Task AddBookAsync(Book book);

        Task RemoveBookAsync(int id);

        Task UpdateBookAsync(Book book);
    }
}