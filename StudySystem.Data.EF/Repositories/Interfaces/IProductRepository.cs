using StudySystem.Data.Entites;
using StudySystem.Data.Models.Data;
using StudySystem.Data.Models.Request;
using StudySystem.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task CreateProduct(List<Product> products);
        Task<ListProductDetailResponseModel> GetAllProduct();
        Task<bool> UpdateProduct(UpdateProductRequestModel updateProduct);
        Task<bool> DeleteProduct(string productId);
        IQueryable<ProductDetailResponseModel> GetProductDetail(string productId);
        Task<ListProductDetailResponseModel> ViewedProduct(ViewedProductRequestModel request);
        Task<ListProductDetailResponseModel> ProductByCategoryId(string categoryId);
        Task<bool> UpdateProductQuantity(UpdateProductQuantityDataModel data);

    }
}
