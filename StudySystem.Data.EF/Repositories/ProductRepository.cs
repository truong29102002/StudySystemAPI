using EFCore.BulkExtensions;
using LinqToDB.Data;
using LinqToDB.EntityFrameworkCore;
using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {

            _context = context;

        }
        public async Task CreateProduct(List<Product> products)
        {
            await _context.BulkInsertOrUpdateAsync(products);
        }
    }
}
