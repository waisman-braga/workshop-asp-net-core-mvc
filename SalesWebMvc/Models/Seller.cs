using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }

        // Implementing association between Seller (N) and Departament (1)
        public Departament Departament { get; set; }

        // departament id is already required bc of type int
        [Display(Name = "Departament")]
        public int DepartamentId { get; set; } 

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
