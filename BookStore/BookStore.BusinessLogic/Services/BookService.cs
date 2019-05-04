using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.UnitOfWork;

namespace BookStore.BusinessLogic.Services
{
    public class BookService: BaseService, IBookService
    {
        public BookService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
            
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await _uow.Books.GetAsync(id);
        }

        public async Task<ICollection<Book>> GetAllBooksAsync()
        {
            return await _uow.Books.GetAllAsync();
        }

        public async Task<ICollection<BookShortDetail>> GetShortDetailBooksAsync()
        {
            var books = await _uow.Books.GetAllAsync();

            var shortDetails = books.Select(i => new BookShortDetail
            {
                Id = i.Id,
                ImageUrl = i.ImageUrl,
                Title = i.Title,
                Price = i.Price,
                BookAuthors = i.BookAuthors
            });

            return shortDetails.ToList();
        }

        public async Task<ICollection<Book>> FindAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _uow.Books.FindAsync(predicate);
        }

        public async Task AddBookAsync(Book book)
        {
            await _uow.Books.AddAsync(book);
            await _uow.SaveAsync();
        }

        public async Task RemoveBookAsync(Book book)
        {
            await _uow.Books.RemoveAsync(book);
            await _uow.SaveAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _uow.Books.UpdateAsync(book);
            await _uow.SaveAsync();
        }
    }
}