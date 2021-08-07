using System.Collections.Generic;
using System;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Departament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Implementing association between Departament (1) and Seller (N)
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        // Constructors default and with arguments && Don't include ICollections attributes
        public Departament() { }

        public Departament(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // Methods
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            // return total sales from departament considering total sales from every seller (method TotalSales implemented on seller class)
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
