using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.UnitOfWork;

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
            var bookEntities = await _uow.Books.GetAllAsync();

            var bookDtos = _mapper.Map<IEnumerable<Book>, ICollection<BookDto>>(bookEntities);

            return bookDtos;
        }

        public async Task<ICollection<BookDto>> GetBooksByCategoryId(int categoryId)
        {
            var bookEntities = await _uow.Books.GetBooksByCategoryId(categoryId);

            var bookDtos = _mapper.Map<IEnumerable<Book>, ICollection<BookDto>>(bookEntities);

            return bookDtos;
        }

        public async Task<ICollection<BookDto>> GetBooksByTitle(string title)
        {
            var bookEntities = await _uow.Books.GetBooksByTitle(title);

            var bookDtos = _mapper.Map<IEnumerable<Book>, ICollection<BookDto>>(bookEntities);

            return bookDtos;
        }

        public async Task AddBookAsync(BookDto book)
        {
            var bookEntity = _mapper.Map<BookDto, Book>(book);
            await _uow.Books.AddAsync(bookEntity);
            await _uow.SaveAsync();

            var bookFromDb = await _uow.Books.GetAsync(bookEntity.Id);

            foreach (var category in book.Categories)
            {
                bookFromDb.BookCategories.Add(new BookCategory
                {
                    BookId = bookFromDb.Id,
                    CategoryId = category.Id
                });
            }

            foreach (var author in book.Authors)
            {
                bookFromDb.BookAuthors.Add(new BookAuthor()
                {
                    BookId = bookFromDb.Id,
                    AuthorId = author.Id
                });
            }

            await _uow.Books.UpdateAsync(bookFromDb);

            await _uow.SaveAsync();

            book.Id = bookEntity.Id;
        }

        public async Task RemoveBookAsync(int id)
        {
            var book = await _uow.Books.GetAsync(id);

            await _uow.Books.RemoveAsync(book);

            await _uow.SaveAsync();
        }

        public async Task UpdateBookAsync(BookDto book)
        {
            var bookEntity = _mapper.Map<BookDto, Book>(book);

            await _uow.Books.UpdateAsync(bookEntity);

            await _uow.SaveAsync();
        }
        
    }
}