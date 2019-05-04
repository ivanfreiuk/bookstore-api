using BookStore.DataAccess.Context;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(StoreDbContext context) : base(context)
        {

        }
    }
}