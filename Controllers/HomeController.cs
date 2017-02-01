using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet_core.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_vsr.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var c = new DataAccess.DatabaseContext();
            Account account = null;
            try
            {
                account = c.Accounts.Include(x => x.Messages).First();
            }
            catch (InvalidOperationException ioex) { }

            ViewData["Account"] = account;  
            ViewData["Message"] = "Welcome";
            return View();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Message model)
        {
            var c = new DataAccess.DatabaseContext();
            if(model != null)
            {
                var a = c.Accounts.Include(x => x.Messages).First();
                model.Account = a;
                a.Messages.Add(model);
                ViewData["Account"] = a;
                c.SaveChanges();
            }
            return View();
        }
    }
}
