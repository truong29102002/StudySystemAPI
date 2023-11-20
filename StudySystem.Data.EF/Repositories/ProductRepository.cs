using EFCore.BulkExtensions;
using LinqToDB.Data;
using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using StudySystem.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<ListProductDetailResponseModel> GetAllProduct()
        {
            ListProductDetailResponseModel model = new ListProductDetailResponseModel();
            var resul1t = (from p in _context.Products
                          join pc in _context.ProductCategories on p.ProductId equals pc.ProductId
                          join c in _context.Categories on pc.CategoryId equals c.CategoryId
                          join i in _context.Images on p.ProductId equals i.ProductId
                          join pc2 in _context.ProductConfigurations on p.ProductId equals pc2.ProductId
                          group new { p, c, i, pc2 } by new { p.ProductId, p.ProductName, p.ProductDescription, p.ProductPrice, p.BrandName, p.ProductQuantity, p.ProductionDate, p.ProductStatus, p.PriceSell, c.CategoryName, c.CategoryId, pc2.Ram, pc2.Rom, pc2.Chip, pc2.Screen } into pGroup
                          select new ProductDetailResponseModel
                          {
                              ProductId = pGroup.Key.ProductId,
                              ProductName = pGroup.Key.ProductName,
                              ProductDescription = pGroup.Key.ProductDescription,
                              ProductPrice = pGroup.Key.ProductPrice,
                              ProductBrand = pGroup.Key.BrandName,
                              ProductQuantity = pGroup.Key.ProductQuantity,
                              ProductSell = pGroup.Key.PriceSell,
                              ProductionDate = pGroup.Key.ProductionDate,
                              ProductStatus = pGroup.Key.ProductStatus,
                              CategoryId = pGroup.Key.CategoryId,
                              ProductCategory = pGroup.Key.CategoryName,
                              Images = pGroup.Select(x => new ImageProductData { ImagePath = x.i.ImageDes }).ToList(),
                              ProductConfig = new ProductConfigData { Chip = pGroup.Key.Chip, Ram = pGroup.Key.Ram, Rom = pGroup.Key.Rom, Screen = pGroup.Key.Screen },
                          }).ToListAsync();
            model.listProductDeatails = await resul1t;
            return model;

        }
    }
}
