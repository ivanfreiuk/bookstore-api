using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Entities;

namespace BookStore.Services.Interfaces
{
    public interface IAuthenticationService
    {
        User Authenticate(string username, string password);
    }
}
