

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.EF;
using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using StudySystem.Data.Models.Request;
using StudySystem.Data.Models.Response;
using StudySystem.Infrastructure.CommonConstant;
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
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWorks;
        private readonly ILogger<UserService> _logger;
        private readonly IAddressUserRepository _addressUserRepository;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWorks = unitOfWork;
            _userRepository = unitOfWork.UserRepository;
            _logger = logger;
            _mapper = mapper;
            _addressUserRepository = unitOfWork.AddressUserRepository;
        }
        /// <summary>
        /// RegisterUserDetail
        /// </summary>
        /// <param name="request"></param>
        /// <returns>bool</returns>
        public async Task<bool> RegisterUserDetail(UserRegisterRequestModel request)
        {
            var createStrategy = _unitOfWorks.CreateExecutionStrategy();
            var result = false;
            await createStrategy.Execute(async () =>
            {
                using (var db = await _unitOfWorks.BeginTransactionAsync())
                {
                    try
                    {
                        var isUserExists = await _userRepository.IsUserExists(request.UserID);
                        if (!isUserExists)
                        {
                            UserDetail userDetail = _mapper.Map<UserDetail>(request);
                            userDetail.Password = PasswordHasher.HashPassword(request.Password);
                            AddressUser addressUser = _mapper.Map<AddressUser>(request.Address);
                            addressUser.Id = Guid.NewGuid();
                            addressUser.UserID = request.UserID;
                            addressUser.CreateUser = request.UserID;
                            addressUser.UpdateUser = request.UserID;
                            if (await _userRepository.InsertUserDetails(userDetail))
                            {
                                if (await _addressUserRepository.InsertUserAddress(addressUser))
                                {
                                    result = true;
                                    await db.CommitAsync();
                                }
                            }
                        }
                        if (!result)
                        {
                            await db.RollbackAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        await db.RollbackAsync();
                        _logger.LogError(ex.Message);

                    }
                }
            });
            return result;
        }
        /// <summary>
        /// DoLogin
        /// </summary>
        /// <param name="request"></param>
        /// <returns>user</returns>
        public async Task<UserDetail> DoLogin(LoginRequestModel request)
        {
            var user = await _userRepository.FindAsync(x => x.UserID.Equals(request.UserID.ToLower())).ConfigureAwait(false);
            if (user != null)
            {
                if (PasswordHasher.VerifyPassword(request.Password, user.Password))
                {
                    return user;
                }
            }
            return null;
        }

        /// <summary>
        /// GetUserById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<UserInformationResponseModel> GetUserById(string id)
        {
            UserInformationResponseModel result = new UserInformationResponseModel();
            try
            {
                var userById = _userRepository.GetUserDetailById(id);
                result.User = (Data.Models.Data.UserDetailDataModel?)userById;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// UserPermissionRolesAuth
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> UserPermissionRolesAuth(string userId)
        {
            var rs = await _userRepository.FindAsync(x => x.UserID.Equals(userId) && x.Role.Equals(int.Parse(Roles.RolesAdmin))).ConfigureAwait(false);
            if (rs != null)
            {
                return true;
            }
            return false;
        }
    }
}
