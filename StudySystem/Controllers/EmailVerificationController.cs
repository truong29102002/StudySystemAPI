using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using StudySystem.Application.Service;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.Models.Request;
using StudySystem.Data.Models.Response;
using StudySystem.Infrastructure.CommonConstant;

namespace StudySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailVerificationController : ControllerBase
    {
        private readonly ISendMailService _sendMailService;
        private readonly string _verifyCode;
        private readonly DateTime _expireCode;
        public EmailVerificationController(ISendMailService sendMailService)
        {
            _sendMailService = sendMailService;
            _verifyCode = new Random().Next(1000, 10000).ToString();
            _expireCode = DateTime.UtcNow.AddMinutes(5);
        }

        [HttpPost(Router.SendMail)]
        public async Task<ActionResult<StudySystemAPIResponse<object>>> SendMail([FromBody] string username)
        {
            var result = await _sendMailService.SendMailAsync(username, _verifyCode);
            return new StudySystemAPIResponse<object>(StatusCodes.Status200OK, new Response<object>(result, new object()));
        }

        [HttpPost("~/api/verify-email")]
        public ActionResult<StudySystemAPIResponse<object>> VerificationEmail(string code)
        {
            var result = _sendMailService.VerificationCode(code, _verifyCode, _expireCode);
            return new StudySystemAPIResponse<object>(StatusCodes.Status200OK, new Response<object>(result, new object()));
        }

    }
}
