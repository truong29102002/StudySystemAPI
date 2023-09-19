﻿using StudySystem.Data.Entites;
using StudySystem.Data.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Application.Service.Interfaces
{
    public interface ILoginUserService : IBaseService
    {
        Task<UserDetail> DoLogin(LoginRequestModel request);
    }
}
