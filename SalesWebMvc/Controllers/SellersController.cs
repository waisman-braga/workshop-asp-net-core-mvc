using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
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

        // implementing create GET action
        public IActionResult Create()
        {
            // return view Name create in seller folder
            return View();
        }

        // implementing create POST action
        // this method get a sellet obj from the request, to get this obj and instance a new seller, we need to put it as a param 
        // the entity framework instances the seller obj
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            // action to insert the obj into the db
            _sellerService.Insert(seller);
            // redirect to the seller index page
            return RedirectToAction(nameof(Index));
        }


    }
}
