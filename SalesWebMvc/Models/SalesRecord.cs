using SalesWebMvc.Models.Enums;
using System;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        // Enum
        public SaleStatus Status { get; set; }
        // Implementing association between SalesRecord (N) and Seller (1)
        public Seller Seller { get; set; }

        // Constructors default and with arguments
        public SalesRecord() { }

        public SalesRecord(int id, double amount, DateTime date, SaleStatus status, Seller seller)
        {
            Id = id;
            Amount = amount;
            Date = date;
            Status = status;
            Seller = seller;
        }
    }
}
