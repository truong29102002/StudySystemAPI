using StudySystem.Data.Entites;
using StudySystem.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF.Repositories.Interfaces
{
    public interface IProductConfigurationRepository : IRepository<ProductConfiguration>
    {
        Task<bool> UpdateProductConfiguration(UpdateProductRequestModel resquest);
        Task<bool> DeleteProductConfiguration(string productId);
    }
}
