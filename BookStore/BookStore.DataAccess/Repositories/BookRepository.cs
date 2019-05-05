using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class BookRepository: BaseRepository<Book>, IBookRepository
    {
        public BookRepository(StoreDbContext context): base(context)
        {
            
        }

        public override async Task<Book> GetAsync(int id)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(a=>a.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(c=>c.Category)
                .Include(b => b.Comments)
                .FirstAsync(i=>i.Id==id);
        }

        public override async Task<ICollection<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(a => a.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(c => c.Category)
                .Include(b => b.Comments)
                .ToListAsync();
        }
    }
}