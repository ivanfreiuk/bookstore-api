using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Identity;
using BookStore.DataAccess.UnitOfWork;

namespace BookStore.BusinessLogic.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
       
        public async Task<User> GetUserAsync(int id)
        {
            return await _uow.Users.GetAsync(id);
        }

        public async Task<ICollection<User>> GetAllUsersAsync()
        {
            return await _uow.Users.GetAllAsync();
        }

        public async Task<ICollection<Role>> GetUserRolesAsync(int userId)
        {
            return await _uow.Users.GetUserRolesAsync(userId);
        }
        
        public async Task AddUserAsync(User user)
        {
            await _uow.Users.AddAsync(user);

            await _uow.SaveAsync();
        }

        public async Task RemoveUserAsync(User user)
        {
            await _uow.Users.RemoveAsync(user);

            await _uow.SaveAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            await _uow.Users.UpdateAsync(user);

            await _uow.SaveAsync();
        }
    }
}
