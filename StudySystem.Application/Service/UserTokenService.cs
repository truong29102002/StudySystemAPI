using Org.BouncyCastle.Math.EC.Rfc7748;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.EF;
using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Application.Service
{
    public class UserTokenService : BaseService, IUserTokenService
    {
        private readonly IUnitOfWork _unitOfWorks;
        private readonly IUserTokenRepository _userTokenRepository;
        public UserTokenService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWorks = unitOfWork;
            _userTokenRepository = unitOfWork.UserTokenRepository;
        }

        public async Task Delete(string userId)
        {
            var userToken = await _userTokenRepository.GetAsync(userId).ConfigureAwait(false);
            if (userToken != null)
            {
                await _userTokenRepository.DeleteAsyn(userToken).ConfigureAwait(false);
                await _unitOfWorks.CommitAsync().ConfigureAwait(false);
            }
        }

        public async Task<ApplicationUserToken> Insert(ApplicationUserToken request)
        {
            await _userTokenRepository.AddAsyn(request).ConfigureAwait(false);
            await _unitOfWorks.CommitAsync().ConfigureAwait (false);
            return request;
        }

        public async Task<bool> IsUserOnl(string userId)
        {
            var checkUserOnl = await _userTokenRepository.FindAllAsync(x => x.UserID.Equals(userId) && x.ExpireTimeOnline > DateTime.UtcNow).ConfigureAwait(false);
            if (checkUserOnl.Any())
            {
                return true;
            }
            return false;
        }
    }
}
