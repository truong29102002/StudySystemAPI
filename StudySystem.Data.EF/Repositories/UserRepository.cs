using Microsoft.EntityFrameworkCore;
using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using StudySystem.Data.Models.Data;
using StudySystem.Infrastructure.CommonConstant;
using StudySystem.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF.Repositories
{
    public class UserRepository : Repository<UserDetail>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// GetUserDetailById
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public UserDetailDataModel GetUserDetailById(string userId)
        {
            var result = (from ud in _context.UserDetails
                          join o in _context.Orders on ud.UserID equals o.UserId into userOrders
                          from uo in userOrders.DefaultIfEmpty()
                          join oi in _context.OrderItems on uo.OrderId equals oi.OrderId into userOrderItems
                          from uoi in userOrderItems.DefaultIfEmpty()
                          where ud.UserID == userId
                          group uoi by new { ud.UserFullName, ud.Email, ud.PhoneNumber, uo.Status, ud.Gender, ud.CreateDateAt } into g
                          select new UserDetailDataModel
                          {
                              UserFullName = g.Key.UserFullName,
                              Email = g.Key.Email,
                              PhoneNumber = g.Key.PhoneNumber,
                              PriceBought = g.Where(x=>g.Key.Status.Equals(StatusOrdetItem.Paid)).Sum(x => x.Price),
                              RankUser = StringUtils.RankUser(g.Where(x => g.Key.Status.Equals(StatusOrdetItem.Paid)).Sum(x => x.Price)),
                              Gender = g.Key.Gender,
                              JoinDateAt = g.Key.CreateDateAt.ToString("dd MM yyyy")
                          }).ToList();
            return result.OrderByDescending(x=>x.PriceBought).FirstOrDefault();
        }

        /// <summary>
        /// InsertUserDetails
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        public async Task<bool> InsertUserDetails(UserDetail userDetail)
        {
            var check = await _context.Set<UserDetail>().SingleOrDefaultAsync(x => x.UserID.Equals(userDetail.UserID.ToLower())).ConfigureAwait(false);
            if (check != null)
            {
                _context.Set<UserDetail>().Remove(check);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            await _context.Set<UserDetail>().AddAsync(userDetail).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
        /// <summary>
        /// IsUserExists
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> IsUserExists(string userId)
        {
            var query = await _context.Set<UserDetail>().SingleOrDefaultAsync(x => x.UserID.Equals(userId.ToLower())).ConfigureAwait(false);
            return query != null;
        }
        /// <summary>
        /// UpdateStatusActiveUser
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task UpdateStatusActiveUser(string userID)
        {
            var query = await _context.Set<UserDetail>().SingleOrDefaultAsync(x => x.UserID.Equals(userID.ToLower())).ConfigureAwait(false);
            if (query != null)
            {
                query.IsActive = true;
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }


    }
}
