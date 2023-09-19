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
        private UserRegisterRepository _userRegisterRepository;
        private LoginUserRepository _loginUserRepository;
        private UserTokenRepository _userTokenRepository;
        public UnitOfWork(AppDbContext context) => _context = context;

        public IUserRegisterRepository UserRegisterRepository
        {
            get { return _userRegisterRepository ?? (_userRegisterRepository = new UserRegisterRepository(_context)); }
        }

        public ILoginUserRepository LoginUserRepository
        {
            get { return _loginUserRepository ?? (_loginUserRepository = new LoginUserRepository(_context)); }
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
