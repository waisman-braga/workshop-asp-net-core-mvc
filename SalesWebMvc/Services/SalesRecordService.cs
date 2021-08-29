using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        // must have a dbContext dependency 
        private readonly SalesWebMvcContext _context;

        // contructor for the injection dependency can happen
        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // implementing operation findAll to return all sellers from db
        public async Task<List<SalesRecord>> FindAllAsync()
        {
            var saleStatus = Enum.GetNames(typeof(SaleStatus)).ToList();
            var result = from obj in _context.SalesRecord select obj;

            result = result.Where(obj => obj.Status == SaleStatus.Billed);

            // get all sellers from db and convert to list (operation sync)
            return await result
                .Include(x => x.Seller) // sales record table join into the saller table
                .Include(x => x.Seller.Departament) // sales record table join into the departament table
                .OrderByDescending(x => x.Date) // orderning by date
                .ToListAsync();
        }

        // method to insert a new sale into the db
        public async Task InsertAsync(SalesRecord obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(obj => obj.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(obj => obj.Date <= maxDate.Value);
            }

            // return a list from the db with the consult
            return await result
                .Include(x => x.Seller) // sales record table join into the saller table
                .Include(x => x.Seller.Departament) // sales record table join into the departament table
                .OrderByDescending(x => x.Date) // orderning by date
                .ToListAsync();
        }

        public async Task<List<IGrouping<Departament,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(obj => obj.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(obj => obj.Date <= maxDate.Value);
            }

            // return a list from the db with the consult
            return await result
                .Include(x => x.Seller) // sales record table join into the saller table
                .Include(x => x.Seller.Departament) // sales record table join into the departament table
                .OrderByDescending(x => x.Date) // orderning by date
                .GroupBy(x => x.Seller.Departament) // when you GroupBy return wont be a List but a IGrouping
                .ToListAsync();
        }

        // method to find a seller into the db
        public async Task<SalesRecord> FindByIdAsync(int id)
        {
            return await _context.SalesRecord.Include(obj => obj.Seller).FirstOrDefaultAsync(obj => obj.Id == id);
        }

    }
}
