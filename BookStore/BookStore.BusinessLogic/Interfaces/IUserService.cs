using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Identity;

namespace BookStore.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int id);
        Task<ICollection<User>> GetAllUsersAsync();
        Task<ICollection<User>> FindAsync(Expression<Func<User, bool>> predicate);

        Task AddUserAsync(User user);

        Task RemoveUserAsync(User user);

        Task UpdateUserAsync(User user);
    }
}
