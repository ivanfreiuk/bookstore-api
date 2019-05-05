using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.Repositories
{
    public class MarkRepository : BaseRepository<Mark>, IMarkRepository
    {
        public MarkRepository(StoreDbContext context) : base(context)
        {

        }

        public async Task<double> GetMarkByBook(int id)
        {
            return  _context.Marks.Where(i=>i.BookId ==id).Select(i=>i).Average(i=>i.MarkValue);
        }
    }
}