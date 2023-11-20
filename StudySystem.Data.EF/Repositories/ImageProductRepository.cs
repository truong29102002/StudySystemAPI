using EFCore.BulkExtensions;
using LinqToDB;
using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF.Repositories
{
    public class ImageProductRepository : Repository<Image>, IImageProductRepository
    {
        private readonly AppDbContext _context;
        public ImageProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> UpdateImageProduct(string productId, List<string> imageName)
        {
            var imgOld = await _context.Set<Image>().Where(x => x.ProductId.Equals(productId) && !imageName.Contains(x.ImageDes)).ToListAsync();
            if (imgOld.Count() > 0)
            {
                await _context.BulkDeleteAsync(imgOld).ConfigureAwait(false);
            }
            //await _context.BulkInsertOrUpdateAsync(dataUpadate);
            return true;
        }
    }
}
