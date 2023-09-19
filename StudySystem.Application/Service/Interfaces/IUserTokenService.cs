using StudySystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Application.Service.Interfaces
{
    public interface IUserTokenService : IBaseService
    {
        Task Delete(string userId);
        Task<ApplicationUserToken> Insert(ApplicationUserToken request);
        Task<bool> IsUserOnl(string userId);
    }
}
