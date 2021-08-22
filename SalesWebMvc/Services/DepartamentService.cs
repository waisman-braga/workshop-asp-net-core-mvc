using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // implementing operation findAll to return all departaments from db
        public List<Departament> FindAll()
        {
            // get all departaments from db, order by name and convert to list (operation sync)
            return _context.Departament.OrderBy(x => x.Name).ToList();
        }



    }
}
