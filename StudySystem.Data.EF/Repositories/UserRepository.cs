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
        public List<UserDetailDataModel> GetUserDetail(string userId)
        {
            var countItem = (from ud in _context.UserDetails
                             join o in _context.Orders on ud.UserID equals o.UserId into userOrders
                             from uo in userOrders.DefaultIfEmpty()
                             join oi in _context.OrderItems on uo.OrderId equals oi.OrderId into userOrderItems
                             from uoi in userOrderItems.DefaultIfEmpty()
                             where ud.UserID == userId && uoi != null
                             select new { }
                            ).Count();
            var address = (from ud in _context.UserDetails
                           join au in _context.Set<AddressUser>() on ud.UserID equals au.UserID
                           join wa in _context.Set<Ward>() on au.WardCode equals wa.Code
                           join dt in _context.Set<District>() on au.DistrictCode equals dt.Code
                           join pr in _context.Set<Province>() on au.ProvinceCode equals pr.Code
                           where ud.UserID == userId
                           select new
                           {
                               Des = au.Descriptions,
                               Ward = wa.Name,
                               Dist = dt.Name,
                               Province = pr.Name,
                           }).FirstOrDefault();
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
                              PriceBought = g.Where(x => g.Key.Status.Equals(StatusOrdetItem.Paid)).Sum(x => x.Price),
                              RankUser = StringUtils.RankUser(g.Where(x => g.Key.Status.Equals(StatusOrdetItem.Paid)).Sum(x => x.Price)),
                              Gender = g.Key.Gender == 0 ? "Nam" : "Nữ",
                              JoinDateAt = g.Key.CreateDateAt.ToString("dd/MM/yyyy"),
                              CountOrderItem = countItem,
                              AddressUserDes = address.Des,
                              AddressUserWard = address.Ward,
                              AddressUserDistrict = address.Dist,
                              AddressUserProvince = address.Province
                          }).ToList();
            return result;
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
