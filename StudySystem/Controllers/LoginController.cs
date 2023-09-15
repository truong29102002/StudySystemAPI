using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.Models.Request;
using StudySystem.Data.Models.Response;
using StudySystem.Infrastructure.CommonConstant;
using StudySystem.Infrastructure.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRegisterService _userRegisterService;
       
        public LoginController(IUserRegisterService userRegisterService)
        {
            _userRegisterService = userRegisterService;
        }
        /// <summary>
        /// RegisterUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Router.RegisterUser)]
        public async Task<ActionResult<StudySystemAPIResponse<object>>> RegisterUser([FromBody]UserRegisterRequestModel request)
        {
            var result = await _userRegisterService.RegisterUserDetail(request);
            return new StudySystemAPIResponse<object>(StatusCodes.Status200OK, new Response<object>(result, new object()));
        }


        [HttpPost(Router.LoginUser)]
        public IActionResult Login([FromBody]LoginRequestModel request)
        {
            return Unauthorized();
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(AppSetting.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
