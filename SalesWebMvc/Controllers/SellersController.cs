using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (!ModelState.IsValid)
            {
                return View(seller);
            }
            // action to insert the obj into the db
            _sellerService.Insert(seller);
            // redirect to the seller index page
            return RedirectToAction(nameof(Index));
        }

        // implementing delete action to open a window where the user can delete or go back to the seller index pg
        public IActionResult Delete(int? id)
        {
            // validate if the id is null, if its null means the request was improper so we return null
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided." });
            }

            // getting the obj we want to delete
            // we need to put id.value bc the id is optional
            var obj = _sellerService.FindById(id.Value);

            // if the id is null return not found
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found." });
            }

            // if everything is right we return the view with the obj as an argument
            return View(obj);
        }

        // implementing delete POST action
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        // implementing details action 
        public IActionResult Details(int? id)
        {
            // validate if the id is null, if its null means the request was improper so we return null
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided." });
            }

            // getting the obj we want to see its details
            // we need to put id.value bc the id is optional
            var obj = _sellerService.FindById(id.Value);

            // if the id is null return not found
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found." });
            }

            // if everything is right we return the view with the obj as an argument
            return View(obj);
        }

        // implementing edit action to open a window where the user can edit or go back to the seller index pg
        public IActionResult Edit(int? id)
        {
            // validate if the id is null, if its null means the request was improper so we return not found
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided." });
            }

            // getting the obj we want to delete
            // we need to put id.value bc the id is optional
            // validate into the db if theres an obj with the id
            var obj = _sellerService.FindById(id.Value);

            // if the obj is null return not found
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found." });
            }

            // openning the edit view 
            // getting the departaments to populate the select box
            // instacing the view model
            List<Departament> departaments = _departamentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departaments = departaments };

            return View(viewModel);
        }

        // implementing edit POST action
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            // validation if its not validated return the view model with all the inputs required and messages 
            // makes validation work even when javascript is unable
            if (!ModelState.IsValid)
            {
                var departaments = _departamentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departaments = departaments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            // this method can throw exceptions 
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e) // ApplicationException abranges DbConcurrencyException and NotFoundException
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // implementing error action to return view error.cshtml
        public IActionResult Error(string message)
        {
            // Instancing view model with the message and the requestId
            var viewModel = new ErrorViewModel
            {
                Message = message,
                // getting the internal ID from the request
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }


    }
}
