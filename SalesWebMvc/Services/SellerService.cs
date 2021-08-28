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
        public async Task<List<Seller>> FindAllAsync()
        {
            // get all sellers from db and convert to list (operation sync)
            return await _context.Seller.ToListAsync();
        }

        // method to insert a new seller into the db
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        // method to find a seller into the db
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Departament).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        // method to remove a seller from the db
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegratyException("Can't delete seller because he/she has sales");
            }
        }

        // method to update a seller from the db
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            // validate if theres this id on db
            if (!hasAny)
            {
                throw new NotFoundException("Id not found - SellerService");
            }

            try
            {
                // when call the Update operation the db may return a concurrency conflict exception 
                // if that exception happen the entity framework will send a DbConcurrencyException we need to put a try and catch to capture that expection
                _context.Update(obj);
                await _context.SaveChangesAsync();
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
