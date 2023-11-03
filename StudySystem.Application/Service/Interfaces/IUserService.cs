using StudySystem.Data.Entites;
using StudySystem.Data.Models.Request;
using StudySystem.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Application.Service.Interfaces
{
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// RegisterUserDetail
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> RegisterUserDetail(UserRegisterRequestModel request);
        /// <summary>
        /// DoLogin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserDetail> DoLogin(LoginRequestModel request);
        /// <summary>
        /// GetUserById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserInformationResponseModel> GetUserById(string id);
        /// <summary>
        /// UserPermissionRolesAuth
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> UserPermissionRolesAuth(string userId);
    }
}
