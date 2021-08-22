using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // dependece for SellerService and DepartamentService
        private readonly SellerService _sellerService;
        private readonly DepartamentService _departamentService;

        // construct to inject depencency
        public SellersController(SellerService sellerService, DepartamentService departamentService)
        {
             _sellerService = sellerService;
            _departamentService = departamentService;
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
            // getting all the departaments
            var departaments = _departamentService.FindAll();
            // instancing the view model
            var viewModel = new SellerFormViewModel { Departaments = departaments };
            // return view Name create in seller folder and the obj viewModel with all departaments populated 
            return View(viewModel);
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
