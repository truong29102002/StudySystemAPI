using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.EF;
using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Models.Request;
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
        public LoginUserService(IUnitOfWork unitOfWork):base(unitOfWork) 
        {
            _unitOfWorkc = unitOfWork;
            _loginUserRepository = unitOfWork.LoginUserRepository;
        }
        public Task<bool> DoLogin(LoginRequestModel request)
        {

            throw new NotImplementedException();
        }
    }
}
