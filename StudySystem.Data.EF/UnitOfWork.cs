using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Storage;
using StudySystem.Data.EF.Repositories;
using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private UserRepository _userRegisterRepository;
        private UserTokenRepository _userTokenRepository;
        private UserVerifycationOTPsRepository _userVerifycationOTPsRepository;
        private LocationRepository<Province> _provincesRepository;
        private LocationRepository<District> _districtsRepository;
        private LocationRepository<Ward> _wardsRepository;
        private AddressUserRepository _addressUserRepository;
        public UnitOfWork(AppDbContext context) => _context = context;

        public IUserRepository UserRepository
        {
            get { return _userRegisterRepository ?? (_userRegisterRepository = new UserRepository(_context)); }
        }


        public IUserTokenRepository UserTokenRepository
        {
            get { return _userTokenRepository ?? (_userTokenRepository = new UserTokenRepository(_context)); }
        }

        public IUserVerificationOTPsRepository UserVerificationOTPsRepository
        {
            get { return _userVerifycationOTPsRepository ?? (_userVerifycationOTPsRepository = new UserVerifycationOTPsRepository(_context)); }
        }

        public ILocationRepository<Province> ProvioncesRepository
        {
            get { return _provincesRepository ?? (_provincesRepository = new LocationRepository<Province>(_context)); }
        }

        public ILocationRepository<District> DistrictsRepository
        {
            get { return _districtsRepository ?? (_districtsRepository = new LocationRepository<District>(_context)); }
        }

        public ILocationRepository<Ward> WardsRepository
        {
            get { return _wardsRepository ?? (_wardsRepository = new LocationRepository<Ward>(_context)); }
        }

        public IAddressUserRepository AddressUserRepository
        {
            get { return _addressUserRepository ?? (_addressUserRepository = new AddressUserRepository(_context)); }
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task BulkInserAsync<T>(IList<T> entities) where T : class
        {
            await _context.BulkInsertAsync(entities).ConfigureAwait(false);
        }

        public async Task<bool> CommitAsync()
        {
            var cm = await _context.SaveChangesAsync().ConfigureAwait(false);
            return cm != 0;
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _context.Database.CreateExecutionStrategy();
        }
    }
}
