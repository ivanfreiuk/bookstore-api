﻿using BookStore.DataAccess.Context;
using BookStore.DataAccess.Identity;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(StoreDbContext context): base(context)
        {
            
        }
    }
}
