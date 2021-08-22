using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        // Class responsible for the data for the create seller form 
        public Seller Seller { get; set; }
        public ICollection<Departament> Departaments { get; set; }
    }
}
