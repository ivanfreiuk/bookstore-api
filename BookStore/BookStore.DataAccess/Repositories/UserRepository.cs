using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.Identity;
using BookStore.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(StoreDbContext context): base(context)
        {
            
        }

        public async Task<ICollection<Role>> GetUserRolesAsync(int userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => _context.Roles.Single(r => r.Id == ur.RoleId))
                .ToListAsync();
        }
    }
}
