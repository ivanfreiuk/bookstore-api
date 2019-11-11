using BookStore.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookStore.DataAccess.Identity
{
    public class User: IdentityUser<int>, IIdentifier
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
