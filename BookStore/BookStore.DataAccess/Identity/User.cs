using System.Collections.Generic;
using BookStore.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookStore.DataAccess.Identity
{
    public class User: IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }
    }
}
