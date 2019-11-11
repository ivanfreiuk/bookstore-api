using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.DataAccess.Identity;

namespace BookStore.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<ICollection<Role>> GetUserRolesAsync(int userId);
    }
}
