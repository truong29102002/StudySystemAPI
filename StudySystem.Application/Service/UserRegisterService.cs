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
    public class UserRegisterService : BaseService, IUserRegisterService
    {
        
        private readonly IUserRegisterRepository _userRegisterRepository;
        private readonly IUnitOfWork _unitOfWorks;
        public UserRegisterService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWorks = unitOfWork;
            _userRegisterRepository = unitOfWork.UserRegisterRepository;
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

                await Console.Out.WriteLineAsync(ex.Message);

            }
            return false;
        }
    }
}
