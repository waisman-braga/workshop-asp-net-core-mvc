using SalesWebMvc.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.ViewModels
{
    public class SalesRecordFormViewModel
    {
        // Class responsible for the data for the create seller form 
        public SalesRecord SalesRecord { get; set; }
        public ICollection<Seller> Sellers { get; set; }




    }
}
