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
using StudySystem.Data.Models.Response;
using StudySystem.Infrastructure.CommonConstant;

namespace StudySystem.Controllers
{
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        public UsersController() { }
        /// <summary>
        /// <para>api: ~/api/list-user-detail</para>
        /// <para>GetUserDetail</para>
        /// </summary>
        /// <returns></returns>
        [HttpGet(Router.GetListUserDetail)]
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
        public ActionResult<StudySystemAPIResponse<object>> GetUserById()
        {
            return new StudySystemAPIResponse<object>(StatusCodes.Status200OK, new Response<object>(true, new object()));
        }
    }
}
