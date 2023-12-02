// <copyright file="ExtensionsController.cs" ownedby="Xuan Truong">
//  Copyright (c) XuanTruong. All rights reserved.
//  FileType: Visual CSharp source file
//  Created On: 29/09/2023
//  Last Modified On: 29/09/2023
//  Description: ExtensionsController.cs
// </copyright>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.Models.Response;

namespace StudySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtensionsController : ControllerBase
    {
        private readonly IExtensionsService _extensionsService;
        private readonly ILogger<ExtensionsController> _logger;
        public ExtensionsController(ILogger<ExtensionsController> logger, IExtensionsService extensionsService)
        {

            _logger = logger;
            _extensionsService = extensionsService;
        }

        [HttpGet("~/api/price-number")]
        public async Task<ActionResult<StudySystemAPIResponse<object>>> ConvertPriceToNumber(long price)
        {
            try
            {
                var rs = await _extensionsService.ConvertPriceToWords(price);
                return new StudySystemAPIResponse<object>(StatusCodes.Status200OK, new Response<object>(true, rs));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BadHttpRequestException("can't convert");
            }


        }

    }
}
