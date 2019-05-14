using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Models;

namespace BookStore.BusinessLogic.Interfaces
{
    public interface IBookService
    {
        Task<BookDto> GetBookAsync(int id);

        Task<ICollection<BookDto>> GetAllBooksAsync();

        Task<ICollection<BookDto>> GetBooksByTitle(string title);

        Task AddBookAsync(BookDto book);

        Task RemoveBookAsync(int id);

        Task UpdateBookAsync(BookDto book);

        Task<ICollection<BookDto>> GetBooksByCategoryId(int categoryId);
    }
}