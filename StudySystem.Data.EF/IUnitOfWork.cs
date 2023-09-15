using StudySystem.Data.EF.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF
{
    public interface IUnitOfWork
    {
        IUserRegisterRepository UserRegisterRepository { get; }
        ILoginUserRepository LoginUserRepository { get; }
        Task<bool> CommitAsync();
    }
}
