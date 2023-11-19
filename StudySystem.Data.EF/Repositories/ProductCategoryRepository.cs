using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF.Repositories
{
    public class ProductCategoryrepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        private readonly AppDbContext _context;
        public ProductCategoryrepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }
        public Task CreateProductCategory(string productId, string categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
