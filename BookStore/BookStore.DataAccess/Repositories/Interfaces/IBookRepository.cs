using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess.Repositories.Interfaces
{
    public interface IBookRepository: IGenericRepository<Book>
    {
        Task<ICollection<Book>> GetBooksByCategoryId(int categoryId);

        Task<ICollection<Book>> GetBooksByTitle(string title);
    }
}