using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using LinqToDB.Data;

namespace StudySystem.Data.EF.Repositories
{
    public class AddressUserRepository : Repository<AddressUser>, IAddressUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public AddressUserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task BulkInsertUsersAddess(AddressUser addressUser)
        {
            //using (var db = _appDbContext.CreateLinqToDbConnection())
            //{
            //await _appDbContext.BulkCopyAsync(new BulkCopyOptions { TableName = "SATConnectorLayoutConnectorInfo" }, addressUser).ConfigureAwait(false);
            //}
            throw new Exception();
        }
    }
}
