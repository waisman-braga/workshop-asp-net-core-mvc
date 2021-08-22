using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        // method to find a seller into the db
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Departament).FirstOrDefault(obj => obj.Id == id);
        }

        // method to remove a seller from the db
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }


    }
}
