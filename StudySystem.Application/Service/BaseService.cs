﻿using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Application.Service
{
    public class BaseService : IBaseService
    {
        protected IUnitOfWork _unitOfWork { get; set; }
        /// <summary>
        /// Baseservice
        /// </summary>
        /// <param name="unitOfWork"></param>
        protected BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
