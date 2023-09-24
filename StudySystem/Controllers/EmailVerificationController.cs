using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudySystem.Application.Service;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.Models.Request;
using StudySystem.Data.Models.Response;
using StudySystem.Infrastructure.CommonConstant;

namespace StudySystem.Controllers
{
    [ApiController]
    [Authorize]
    public class EmailVerificationController : ControllerBase
    {
        private readonly ISendMailService _sendMailService;
        private readonly string _verifyCode;
        private readonly DateTime _expireCode;
        public EmailVerificationController(ISendMailService sendMailService)
        {
            _sendMailService = sendMailService;
            
        }

        [HttpPost(Router.SendMail)]
        public async Task<ActionResult<StudySystemAPIResponse<object>>> SendMail([FromBody] string username)
        {
            var result = await _sendMailService.SendMailAsync(username, _verifyCode);
            return new StudySystemAPIResponse<object>(StatusCodes.Status200OK, new Response<object>(result, new object()));
        }

        [HttpPost(Router.VerificationEmail)]
        public ActionResult<StudySystemAPIResponse<object>> VerificationEmail(string code)
        {
            var result = _sendMailService.VerificationCode(code, _verifyCode, _expireCode);
            return new StudySystemAPIResponse<object>(StatusCodes.Status200OK, new Response<object>(result, new object()));
        }

    }
}
