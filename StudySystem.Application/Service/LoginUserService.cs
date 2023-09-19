using Microsoft.Extensions.Logging;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.EF;
using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using StudySystem.Data.Models.Request;
using StudySystem.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Application.Service
{
    public class LoginUserService : BaseService, ILoginUserService
    {
        private readonly ILoginUserRepository _loginUserRepository;
        private readonly IUnitOfWork _unitOfWorkc;
        private readonly ILogger<LoginUserService> _logger;
        public LoginUserService(IUnitOfWork unitOfWork, ILogger<LoginUserService> logger) : base(unitOfWork)
        {
            _unitOfWorkc = unitOfWork;
            _logger = logger;
            _loginUserRepository = unitOfWork.LoginUserRepository;
        }
        public async Task<UserDetail> DoLogin(LoginRequestModel request)
        {
            var user = await _loginUserRepository.FindAsync(x => x.UserID.Equals(request.UserID.ToLower())).ConfigureAwait(false);

            if (user == null)
            {
                return null;
            }
            if (PasswordHasher.VerifyPassword(request.Password, user.Password))
            {
                return user;
            }
            return null;
        }
    }
}
