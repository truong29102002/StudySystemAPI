using StudySystem.Data.Models.Request;
using StudySystem.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Application.Service.Interfaces
{
    public interface IProductService : IBaseService
    {
        Task<string> CreateProduct(CreateProductRequestModel request, List<string> imageName);
        Task<ListProductDetailResponseModel> GetAllProductDetails();
        Task<bool> UpdateProductDetail(UpdateProductRequestModel request, List<string> imageNew);
    }
}
