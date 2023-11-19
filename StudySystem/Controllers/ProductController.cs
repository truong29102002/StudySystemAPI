// <copyright file="ProductController.cs" ownedby="Xuan Truong">
//  Copyright (c) XuanTruong. All rights reserved.
//  FileType: Visual CSharp source file
//  Created On: 29/09/2023
//  Last Modified On: 29/09/2023
//  Description: ProductController.cs
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.Models.Data;
using StudySystem.Data.Models.Request;
using StudySystem.Data.Models.Response;
using StudySystem.Infrastructure.CommonConstant;
using StudySystem.Infrastructure.Resources;
using StudySystem.Middlewares;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StudySystem.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        public static IWebHostEnvironment _environment;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IWebHostEnvironment environment, IProductService productService)
        {
            _logger = logger;
            _environment = environment;
            _productService = productService;
        }

        /// <summary>
        /// CreateProduct
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Router.CreateProduct)]
        [Authorize]
        [AuthPermission]
        public async Task<ActionResult<StudySystemAPIResponse<object>>> CreateProduct([FromForm] CreateProductRequestModel request)
        {
            List<string> pdImgs = ListImageName(request);
            var rs = await _productService.CreateProduct(request, pdImgs);
            if (!rs)
            {
                throw new BadHttpRequestException(Message.ErrorCreateProduct);
            }
            else
            {
                try
                {
                    string Filepath = GetFilepath();
                    if (!System.IO.Directory.Exists(Filepath))
                    {
                        System.IO.Directory.CreateDirectory(Filepath);
                    }
                    foreach (var file in request.ImageProducts)
                    {
                        string imagepath = Filepath + "\\" + file.FileName;
                        if (System.IO.File.Exists(imagepath))
                        {
                            System.IO.File.Delete(imagepath);
                        }
                        using (FileStream stream = System.IO.File.Create(imagepath))
                        {
                            file.CopyToAsync(stream);
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

            }
            return new StudySystemAPIResponse<object>(StatusCodes.Status200OK, new Response<object>(rs, new object()));
        }




        private List<string> GetImage()
        {
            List<string> Imageurl = new List<string>();
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = GetFilepath();

                if (System.IO.Directory.Exists(Filepath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string filename = fileInfo.Name;
                        string imagepath = Filepath + "\\" + filename;
                        if (System.IO.File.Exists(imagepath))
                        {
                            string _Imageurl = hosturl + "/product/" + filename;
                            Imageurl.Add(_Imageurl);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Imageurl;
        }

        [NonAction]
        private List<string> ListImageName(CreateProductRequestModel objFile)
        {
            List<string> list = new List<string>();
            try
            {
                foreach (var file in objFile.ImageProducts)
                {
                    list.Add(file.FileName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
            return list;
        }

        [NonAction]
        private string GetFilepath()
        {
            return _environment.WebRootPath + "\\product\\";
        }
    }
}
