using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // dependece for SellerService
        private readonly SellerService _sellerService;

        // construct to inject depencency
        public SellersController(SellerService sellerService)
        {
             _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            // controller (SellersController) accessed the model (SellerService) to get data from list and send the data to the view (Seller/View/Index)
            // implementing Index method, which call SellerService.FindAll
            // return list of sellers and sending them to view
            var list = _sellerService.FindAll();
            return View(list);
        }

        
    }
}
