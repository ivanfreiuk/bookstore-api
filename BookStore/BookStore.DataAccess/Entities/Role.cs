using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BookStore.DataAccess.Entities
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<User> Users { get; set; }
    }
}
