using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        // dependece for SellerService and DepartamentService
        private readonly SellerService _sellerService;

        public SalesRecordsController(SalesRecordService salesRecordService, SellerService sellerService)
        {
            _salesRecordService = salesRecordService;
            _sellerService = sellerService;
        }

        public async Task<IActionResult> Index()
        {
            // controller (SellersController) accessed the model (SellerService) to get data from list and send the data to the view (Seller/View/Index)
            // implementing Index method, which call SellerService.FindAll
            // return list of sellers and sending them to view
            var list = await _salesRecordService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            // sending the minDate and maxDate to the new by the view data directionary
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            // caling the method from the service
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }


        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            // sending the minDate and maxDate to the new by the view data directionary
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            // caling the method from the service
            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }

        // implementing create GET action
        public async Task<IActionResult> Create()
        {
            // getting all the departaments
            var sellers = await _sellerService.FindAllAsync();
            // instancing the view model
            var viewModel = new SalesRecordFormViewModel { Sellers = sellers };
            // return view Name create in seller folder and the obj viewModel with all departaments populated 
            return View(viewModel);
        }

        // implementing create POST action
        // this method get a sellet obj from the request, to get this obj and instance a new seller, we need to put it as a param 
        // the entity framework instances the seller obj
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesRecord salesRecord)
        {
            // validation if its not validated return the view model with all the inputs required and messages 
            // makes validation work even when javascript is unable
            if (!ModelState.IsValid)
            {
                var sellers = await _sellerService.FindAllAsync();
                var viewModel = new SalesRecordFormViewModel {SalesRecord = salesRecord, Sellers = sellers };
                return View(viewModel);
            }
            // action to insert the obj into the db
            await _salesRecordService.InsertAsync(salesRecord);
            // redirect to the seller index page
            return RedirectToAction(nameof(Index));
        }

        // implementing edit action to open a window where the user can edit or go back to the seller index pg
        public async Task<IActionResult> Edit(int? id)
        {
            // validate if the id is null, if its null means the request was improper so we return not found
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided." });
            }

            // getting the obj we want to delete
            // we need to put id.value bc the id is optional
            // validate into the db if theres an obj with the id
            var obj = await _salesRecordService.FindByIdAsync(id.Value);

            // if the obj is null return not found
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found." });
            }

            // openning the edit view 
            // getting the departaments to populate the select box
            // instacing the view model
            List<Seller> sellers = await _sellerService.FindAllAsync();
            SalesRecordFormViewModel viewModel = new SalesRecordFormViewModel { SalesRecord = obj, Sellers = sellers };

            return View(viewModel);
        }

        private object Error()
        {
            throw new NotImplementedException();
        }
    }
}
