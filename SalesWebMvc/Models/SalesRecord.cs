using SalesWebMvc.Models.Enums;
using System;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        // Enum
        public SaleStatus Status { get; set; }
        // Implementing association between SalesRecord (N) and Seller (1)
        public Seller Seller { get; set; }

        // Constructors default and with arguments
        public SalesRecord() { }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }        

    }
}
