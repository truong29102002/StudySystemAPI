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
    public class LoginUserRepository : Repository<UserDetail>, ILoginUserRepository
    {
        private readonly AppDbContext _context;
        public LoginUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<UserDetail> GetUser(string username)
        {
            return await _context.Set<UserDetail>().SingleOrDefaultAsync(x=>x.Username.Equals(username)).ConfigureAwait(false);
        }
    }
}
