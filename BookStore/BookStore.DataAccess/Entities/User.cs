using Microsoft.AspNetCore.Identity;

namespace BookStore.DataAccess.Entities
{
    public class User: IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
