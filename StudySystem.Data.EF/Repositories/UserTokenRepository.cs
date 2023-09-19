using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF.Repositories
{
    public class UserTokenRepository : Repository<ApplicationUserToken>,IUserTokenRepository
    {
        private readonly AppDbContext _context;
        public UserTokenRepository(AppDbContext context) : base(context) 
        {

            _context = context;

        }

       
    }
}
