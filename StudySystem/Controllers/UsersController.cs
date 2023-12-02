// <copyright file="UsersController.cs" ownedby="Xuan Truong">
//  Copyright (c) XuanTruong. All rights reserved.
//  FileType: Visual CSharp source file
//  Created On: 21/10/2023
//  Last Modified On: 21/10/2023
//  Description: UsersController.cs
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.EF;
using StudySystem.Data.Models.Response;
using StudySystem.Infrastructure.CommonConstant;
using StudySystem.Middlewares;

namespace StudySystem.Controllers
{
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        /// <summary>
        /// <para>api: ~/api/list-user-detail</para>
        /// <para>GetUserDetail</para>
        /// </summary>
        /// <returns></returns>
        [HttpGet(Router.GetListUserDetail)]
        [AuthPermission]
        public ActionResult<StudySystemAPIResponse<object>> GetListUserDetail()
        {
            return new StudySystemAPIResponse<object>(StatusCodes.Status200OK, new Response<object>(true, new object()));
        }
        /// <summary>
        /// <para>api: ~/api/get-user-by-id</para>
        /// <para>GetUserById</para>
        /// </summary>
        /// <returns></returns>
        [HttpGet(Router.GetUserById)]
        public async Task<ActionResult<StudySystemAPIResponse<UserInformationResponseModel>>> GetUserById()
        {
            var rs = await _userService.GetUserById();
            return new StudySystemAPIResponse<UserInformationResponseModel>(StatusCodes.Status200OK, new Response<UserInformationResponseModel>(true, rs));
        }
    }
}
