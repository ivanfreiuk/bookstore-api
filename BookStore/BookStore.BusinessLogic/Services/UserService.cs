using System.Collections.Generic;
using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.UnitOfWork;

namespace BookStore.BusinessLogic.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        
        public User GetUser(int id)
        {
            return _uow.Users.Get(id);
        }

        public ICollection<User> GetAllUsers()
        {
            return _uow.Users.GetAll();
        }

        public void Create(User user)
        {
            _uow.Users.Add(user);
        }

        public void Update(User user)
        {
            _uow.Users.Update(user);
        }

        public void Remove(User user)
        {
            _uow.Users.Remove(user);
        }
    }
}
