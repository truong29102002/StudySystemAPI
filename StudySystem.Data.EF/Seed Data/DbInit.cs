using Microsoft.Extensions.Logging;
using StudySystem.Data.EF.Seed_Data.ClassMap;
using StudySystem.Data.Entites;
using StudySystem.Infrastructure.CommonConstant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Data.EF.Seed_Data
{
    public class DbInit
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DbInit> _logger;
        public DbInit(AppDbContext context, ILogger<DbInit> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task Seed()
        {
            GeneratorFile generatorFile = new GeneratorFile();
            try
            {

                if (!_context.UserDetails.Any())
                {
                    List<UserDetail> userDetails = generatorFile.CsvDataGenerator<UserDetail, UserDetailMap>(CommonConstant.CsvFileUserDetails);
                    await _context.AddRangeAsync(userDetails).ConfigureAwait(false);
                }
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

        }
    }
}
