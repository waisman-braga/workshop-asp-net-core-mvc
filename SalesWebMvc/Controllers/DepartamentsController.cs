using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class DepartamentsController : Controller
    {
        public IActionResult Index()
        {
            List<Departament> departaments = new List<Departament>();
            departaments.Add(new Departament { Id = 1, Name = "Eletronics" });
            departaments.Add(new Departament { Id = 2, Name = "Fashion" });

            return View(departaments);
        }
    }
}
