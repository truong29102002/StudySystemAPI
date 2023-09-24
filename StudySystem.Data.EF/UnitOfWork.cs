using StudySystem.Data.EF.Repositories;
using StudySystem.Data.EF.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private UserRepository _userRegisterRepository;
        private UserTokenRepository _userTokenRepository;
        public UnitOfWork(AppDbContext context) => _context = context;

        public IUserRepository UserRepository
        {
            get { return _userRegisterRepository ?? (_userRegisterRepository = new UserRepository(_context)); }
        }


        public IUserTokenRepository UserTokenRepository
        {
            get { return _userTokenRepository ?? (_userTokenRepository = new UserTokenRepository(_context)); }
        }

        public async Task<bool> CommitAsync()
        {
            var cm = await _context.SaveChangesAsync().ConfigureAwait(false);
            return cm != 0;
        }
    }
}
