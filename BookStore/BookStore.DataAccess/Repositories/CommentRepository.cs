using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(StoreDbContext context) : base(context)
        {

        }

        public override async Task<Comment> GetAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.Book)
                .Include(c => c.User)
                .FirstAsync(c=>c.Id==id);
        }

        public override async Task<ICollection<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .Include(c => c.Book)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<ICollection<Comment>> GetCommentsByBookIdAsync(int bookId)
        {
            return await _context.Comments
                .Where(c => c.BookId == bookId)
                .Select(c => c).ToListAsync();
        }

        public double GetMarkByBookId(int bookId)
        {
            var comments = _context.Comments
                .Where(c => c.BookId == bookId)
                .Select(c => c.Mark);

            if (comments.Any())
            {
                return comments.Average();
            }

            return 0;
        }
    }
}