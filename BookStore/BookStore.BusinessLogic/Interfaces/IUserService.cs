using System.Collections.Generic;
using BookStore.DataAccess.Entities;

namespace BookStore.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        User GetUser(int id);

        ICollection<User> GetAllUsers();

        void Create(User user);

        void Update(User user);

        void Remove(User user);
    }
}
