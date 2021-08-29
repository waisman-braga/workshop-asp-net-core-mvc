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

        public IActionResult Index()
        {
            return View();
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


    }
}
