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
using LinqToDB.EntityFrameworkCore;

namespace StudySystem.Data.EF.Repositories
{
    public class AddressUserRepository : Repository<AddressUser>, IAddressUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public AddressUserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [Obsolete]
        public async Task BulkInsertUsersAddess(List<AddressUser> addressUsers)
        {
            using (var db = _appDbContext.CreateLinqToDbConnection())
            {
                await db.BulkCopyAsync(new BulkCopyOptions { TableName = "AddressUsers" }, addressUsers).ConfigureAwait(false);
            }
        }
    }
}
