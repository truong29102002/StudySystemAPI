using StudySystem.Data.Models.Request;
using StudySystem.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Application.Service.Interfaces
{
    public interface ICartService : IBaseService
    {
        Task<bool> InsertOrUpdateCart(CartInsertRequestModel model);
        Task<CartResponseModel> GetCart();
    }
}
