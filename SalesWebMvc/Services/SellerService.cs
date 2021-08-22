using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        // must have a dbContext dependency 
        private readonly SalesWebMvcContext _context;

        // contructor for the injection dependency can happen
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // implementing operation findAll to return all sellers from db
        public List<Seller> FindAll()
        {
            // get all sellers from db and convert to list (operation sync)
            return _context.Seller.ToList();
        }

        // method to insert a new seller into the db
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }


    }
}
