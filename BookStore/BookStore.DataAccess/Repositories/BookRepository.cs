using BookStore.DataAccess.Context;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.Repositories
{
    public class BookRepository: BaseRepository<Book>, IBookRepository
    {
        public BookRepository(StoreDbContext context): base(context)
        {
            
        }
    }
}