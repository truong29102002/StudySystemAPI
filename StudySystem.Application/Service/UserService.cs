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
    public class UserService : BaseService, IUserService
    {
        
        private readonly IUserRepository _userRegisterRepository;
        private readonly IUnitOfWork _unitOfWorks;
        private readonly ILogger<UserService> _logger;
        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger) : base(unitOfWork)
        {
            _unitOfWorks = unitOfWork;
            _userRegisterRepository = unitOfWork.UserRepository;
            _logger = logger;
        }

        public async Task<bool> RegisterUserDetail(UserRegisterRequestModel request)
        {
            try
            {
                var isUserExists = await _userRegisterRepository.IsUserExists(request.UserID);
                if (!isUserExists)
                {
                    UserDetail userDetail = new UserDetail();
                    userDetail.UserID = request.UserID.ToLower();
                    userDetail.Password = PasswordHasher.HashPassword(request.Password);
                    userDetail.Email = request.Email.ToLower();
                    userDetail.Address = request.Address;
                    userDetail.PhoneNumber = request.PhoneNumber;
                    userDetail.Gender = request.Gender;
                    userDetail.Role = 0;
                    userDetail.UserFullName = request.FullName.ToLower();
                    userDetail.IsActive = false;
                    await _userRegisterRepository.InsertUserDetails(userDetail);
                    return true;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);

            }
            return false;
        }
        public async Task<UserDetail> DoLogin(LoginRequestModel request)
        {
            var user = await _userRegisterRepository.FindAsync(x => x.UserID.Equals(request.UserID.ToLower())).ConfigureAwait(false);
            if (user != null)
            {
                if (PasswordHasher.VerifyPassword(request.Password, user.Password))
                {
                    return user;
                }
            }
            return null;
        }
    }
}
