using System.Threading.Tasks;
using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess.Repositories.Interfaces
{
    public interface IMarkRepository: IGenericRepository<Mark>
    {
        Task<double> GetMarkByBook(int id);
    }
}
