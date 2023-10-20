

using AutoMapper;
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
    /// <summary>
    /// UserService
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRegisterRepository;
        private readonly IUnitOfWork _unitOfWorks;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWorks = unitOfWork;
            _userRegisterRepository = unitOfWork.UserRepository;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// RegisterUserDetail
        /// </summary>
        /// <param name="request"></param>
        /// <returns>bool</returns>
        public async Task<bool> RegisterUserDetail(UserRegisterRequestModel request)
        {
            try
            {
                var isUserExists = await _userRegisterRepository.IsUserExists(request.UserID);
                if (!isUserExists)
                {
                    UserDetail userDetail = _mapper.Map<UserDetail>(request);
                    AddressUser addressUser = _mapper.Map<AddressUser>(request.Address);
                    addressUser.Id = Guid.NewGuid();
                    addressUser.CreateUser = request.UserID;
                    addressUser.UpdateUser = request.UserID;
                    await Console.Out.WriteLineAsync("a");
                    //await _userRegisterRepository.InsertUserDetails(userDetail);
                    return true;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);

            }
            return false;
        }
        /// <summary>
        /// DoLogin
        /// </summary>
        /// <param name="request"></param>
        /// <returns>user</returns>
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
