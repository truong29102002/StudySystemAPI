using StudySystem.Data.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Application.Service.Interfaces
{
    public interface IProductService : IBaseService
    {
        Task<bool> CreateProduct(CreateProductRequestModel request, List<string> imageName);
    }
}
