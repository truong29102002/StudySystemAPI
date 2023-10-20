using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IUserTokenRepository UserTokenRepository { get; }
        IUserVerificationOTPsRepository UserVerificationOTPsRepository { get; }
        ILocationRepository<Province> ProvioncesRepository { get; }
        ILocationRepository<District> DistrictsRepository { get; }
        ILocationRepository<Ward> WardsRepository { get; }
        IAddressUserRepository AddressUserRepository { get; }
        Task<bool> CommitAsync();
    }
}
