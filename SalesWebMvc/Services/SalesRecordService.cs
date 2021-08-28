using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

    }
}
