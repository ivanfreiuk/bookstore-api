using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BookStore.DataAccess.Identity
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<User> Users { get; set; }
    }
}
