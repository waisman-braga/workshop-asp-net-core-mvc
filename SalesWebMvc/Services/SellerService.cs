using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SalesWebMvc.Services.Exceptions;

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

        // method to update a seller from the db
        public void Update(Seller obj)
        {
            // validate if theres this id on db
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found - SellerService");
            }

            try
            {
                // when call the Update operation the db may return a concurrency conflict exception 
                // if that exception happen the entity framework will send a DbConcurrencyException we need to put a try and catch to capture that expection
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbConcurrencyException e)
            {
                // service exception to get the message from the db 
                // Intercepting an access db exception and lauching this exception using my service level exception Services/Exceptions/DbConcurrencyException
                // my service won't proprague an exception from a data base acess
                // if an access db exception happen my service will throw an exception from its own service not from the db
                // SellersController will only throw exceptions from the service leve 
                // Data base exceptions are captured from the service and throw to the controller
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
