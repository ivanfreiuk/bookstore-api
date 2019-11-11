using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(StoreDbContext context) : base(context)
        {

        }

        public override async Task<Book> GetAsync(int id)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(a => a.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(c => c.Category)
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(i => i.Id == id);
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

        public async Task<ICollection<Book>> GetBooksByCategoryId(int categoryId)
        {
            return await _context.Books
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .Where(b => b.BookCategories.Any(bc => bc.CategoryId == categoryId && bc.BookId == b.Id))
                .Select(b => b).ToListAsync();
        }

        public async Task<ICollection<Book>> GetBooksByTitle(string title)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(a => a.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(c => c.Category)
                .Include(b => b.Comments)
                .Where(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public override async Task UpdateAsync(Book book)
        {
            var bookFromDb = await _context.Books.FirstOrDefaultAsync(b=> b.Id==book.Id);

            bookFromDb.Title = book.Title;
            bookFromDb.Price = book.Price;
            bookFromDb.Description = book.Description;
            bookFromDb.ImageUrl = book.ImageUrl;
            bookFromDb.Language = book.Language;
            bookFromDb.CommentsEnabled = book.CommentsEnabled;
            bookFromDb.PageSize = book.PageSize;

            _context.Books.Update(bookFromDb);
        }
    }
}