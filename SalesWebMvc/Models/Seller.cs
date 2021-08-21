using System;
using System.Linq;
using System.Collections.Generic;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        // Implementing association between Seller (N) and Departament (1)
        public Departament Departament { get; set; }
        // Implementing association between Seller (1) and SalesRecord (N)
        public ICollection<SalesRecord> SalesRecord { get; set; } = new List<SalesRecord>();

        // Constructors default and with arguments & Don't include ICollections attributes
        public Seller() { }

        public Seller(int id, string name, string email, double baseSalary, DateTime birthDate, Departament departament)
        {
            Id = id;
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDate = birthDate;
            Departament = departament;
        }

        // Methods 
        public void AddSalesRecord(SalesRecord salesRecord)
        {
            SalesRecord.Add(salesRecord);
        }

        public void RemoveSalesRecord(SalesRecord salesRecord)
        {
            SalesRecord.Remove(salesRecord);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            // Return total sales between initial and final date
            return SalesRecord.Where(sale => sale.Date >= initial && sale.Date <= final).Sum(sale => sale.Amount);
        }

    }
}
