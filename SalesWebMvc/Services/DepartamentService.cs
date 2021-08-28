using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartamentService
    {
        // must have a dbContext dependency 
        private readonly SalesWebMvcContext _context;

        // contructor for the injection dependency can happen
        public DepartamentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // implementing operation findAllAsync to return all departaments from db
        public async Task<List<Departament>> FindAllAsync()
        {
            // get all departaments from db, order by name and convert to list (operation sync)
            // the linq prepares the consult on db
            // await tells the compiler that its an async call
            // this execution wont block the aplication 
            return await _context.Departament.OrderBy(x => x.Name).ToListAsync();
        }



    }
}
