using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.UnitOfWork;
using Remotion.Linq.Utilities;

namespace BookStore.BusinessLogic.Services
{
    public class BookService: BaseService, IBookService
    {
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<BookDto> GetBookAsync(int id)
        {
            var bookEntity = await _uow.Books.GetAsync(id);

            var bookDto = _mapper.Map<Book, BookDto>(bookEntity);

            return bookDto;
        }

        public async Task<ICollection<BookDto>> GetAllBooksAsync()
        {
            var bookEnteties = await _uow.Books.GetAllAsync();

            var bookDtos = _mapper.Map<ICollection<Book>, ICollection<BookDto>>(bookEnteties);

            return bookDtos;
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
        
        public async Task AddBookAsync(Book book)
        {
            await _uow.Books.AddAsync(book);

            await _uow.SaveAsync();
        }

        public async Task RemoveBookAsync(int id)
        {
            var book = await _uow.Books.GetAsync(id);

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