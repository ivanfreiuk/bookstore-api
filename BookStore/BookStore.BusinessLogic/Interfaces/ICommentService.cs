using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Models;

namespace BookStore.BusinessLogic.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto> GetCommentAsync(int id);

        Task<ICollection<CommentDto>> GetAllCommentsAsync();

        Task<ICollection<CommentDto>> GetCommentsByBookIdAsync(int bookId);

        Task AddCommentAsync(CommentDto comment);

        Task RemoveCommentAsync(int id);

        Task UpdateCommentAsync(CommentDto comment);
    }
}