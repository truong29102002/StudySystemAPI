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

                if (!_context.AdministrativeRegions.Any())
                {
                    List<AdministrativeRegion> administrativeRegions = generatorFile.CsvDataGenerator<AdministrativeRegion, AdministrativeRegonMap>(CommonConstant.CsvAdministrativeRegions);
                    await _context.AddRangeAsync(administrativeRegions).ConfigureAwait(false);
                }

                if (!_context.AdministrativeUnits.Any())
                {
                    List<AdministrativeUnit> administrativeUnits = generatorFile.CsvDataGenerator<AdministrativeUnit, AdministrativeUnitMap>(CommonConstant.CsvAdministrativeUnits);
                    await _context.AddRangeAsync(administrativeUnits).ConfigureAwait(false);
                }

                if (!_context.Provinces.Any())
                {
                    List<Province> provinces = generatorFile.CsvDataGenerator<Province, ProvinceMap>(CommonConstant.CsvProvinces);
                    await _context.AddRangeAsync(provinces).ConfigureAwait(false);
                }

                if (!_context.Districts.Any())
                {
                    List<District> districts = generatorFile.CsvDataGenerator<District, DistrictMap>(CommonConstant.CsvDistricts);
                    await _context.AddRangeAsync(districts).ConfigureAwait(false);
                }

                if (!_context.Wards.Any())
                {
                    List<Ward> wards = generatorFile.CsvDataGenerator<Ward, WardMap>(CommonConstant.CsvWards);
                    await _context.AddRangeAsync(wards).ConfigureAwait(false);
                }

                await _context.SaveChangesAsync().ConfigureAwait(false);
                _logger.LogInformation("Init Complement to db");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

        }
    }
}
