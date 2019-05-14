using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess.Repositories.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        double GetMarkByBookId(int bookId);
        Task<ICollection<Comment>> GetCommentsByBookIdAsync(int bookId);
    }
}