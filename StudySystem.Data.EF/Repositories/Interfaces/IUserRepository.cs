using StudySystem.Data.Entites;
using StudySystem.Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<UserDetail>
    {
        Task<bool> IsUserExists(string userId);
        Task<bool> InsertUserDetails(UserDetail userDetail);
        Task UpdateStatusActiveUser(string userID);
        UserDetailDataModel  GetUserDetailById(string userId);
    }
}
