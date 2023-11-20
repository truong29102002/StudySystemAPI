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
using Org.BouncyCastle.Asn1.Ocsp;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.Models.Data;
using StudySystem.Data.Models.Request;
using StudySystem.Data.Models.Response;
using StudySystem.Infrastructure.CommonConstant;
using StudySystem.Infrastructure.Resources;
using StudySystem.Middlewares;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StudySystem.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IWebHostEnvironment _environment;
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
        //[Authorize]
        //[AuthPermission]
        public async Task<ActionResult<StudySystemAPIResponse<object>>> CreateProduct([FromForm] CreateProductRequestModel request)
        {
            List<string> pdImgs = ListImageName(request.ImageProducts);
            var rs = await _productService.CreateProduct(request, pdImgs);
            if (rs == "")
            {
                throw new BadHttpRequestException(Message.ErrorCreateProduct);
            }
            else
            {
                try
                {
                    string Filepath = GetFilepath(rs);
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
                            await file.CopyToAsync(stream);
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

            }
            return new StudySystemAPIResponse<object>(StatusCodes.Status200OK, new Response<object>(true, rs));
        }

        /// <summary>
        /// GetAllProduct
        /// api: "~/api/get-product"
        /// </summary>
        /// <returns></returns>
        [HttpGet(Router.GetAllProduct)]
        public async Task<ActionResult<StudySystemAPIResponse<ListProductDetailResponseModel>>> GetAllProduct()
        {
            var rs = await _productService.GetAllProductDetails();
            string hosturl = $"{this.Request.Scheme}:/{this.Request.Host}{this.Request.PathBase}/Product/";
            foreach (var productDetail in rs.listProductDeatails)
            {
                foreach (var image in productDetail.Images)
                {
                    image.ImagePath = hosturl + $"{productDetail.ProductId}/" + image.ImagePath;
                }
            }
            return new StudySystemAPIResponse<ListProductDetailResponseModel>(StatusCodes.Status200OK, new Response<ListProductDetailResponseModel>(true, rs));
        }

        /// <summary>
        /// UpdateProduct
        /// "~/api/update-product"
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut(Router.UpdateProduct)]
        //[Authorize]
        //[AuthPermission]
        public async Task<ActionResult<StudySystemAPIResponse<object>>> UpdateProduct([FromForm] UpdateProductRequestModel requestModel)
        {
            List<string> pdImgs = ListImageName(requestModel.ImageProducts);
            await _productService.UpdateProductDetail(requestModel, pdImgs);
            return new StudySystemAPIResponse<object>(StatusCodes.Status200OK, new Response<object>(true, new object()));
        }

        [NonAction]
        private List<string> ListImageName(IFormFileCollection objFile)
        {
            List<string> list = new List<string>();
            try
            {
                foreach (var file in objFile)
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
        private string GetFilepath(string productId)
        {
            return _environment.WebRootPath + "\\product\\" + productId;
        }
    }
}
