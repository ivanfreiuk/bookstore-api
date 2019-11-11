using System.Collections.Generic;
using BookStore.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookStore.DataAccess.Identity
{
    public class Role : IdentityRole<int>, IIdentifier
    {
        public virtual ICollection<User> Users { get; set; }
    }
}
