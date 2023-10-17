using Microsoft.EntityFrameworkCore;
using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF.Repositories
{
    public class UserRepository : Repository<UserDetail>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {

            _context = context;

        }

        public async Task InsertUserDetails(UserDetail userDetail)
        {
            var check = await _context.Set<UserDetail>().SingleOrDefaultAsync(x => x.UserID.Equals(userDetail.UserID.ToLower())).ConfigureAwait(false);
            if (check != null)
            {
                _context.Set<UserDetail>().Remove(check);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            await _context.Set<UserDetail>().AddAsync(userDetail).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<bool> IsUserExists(string userId)
        {
            var query = await _context.Set<UserDetail>().SingleOrDefaultAsync(x => x.UserID.Equals(userId.ToLower())).ConfigureAwait(false);
            return query != null;
        }

        public async Task UpdateStatusActiveUser(string userID)
        {
            var query = await _context.Set<UserDetail>().SingleOrDefaultAsync(x => x.UserID.Equals(userID.ToLower())).ConfigureAwait(false);
            if (query != null)
            {
                query.IsActive = true;
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
