using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using dotnet_core.Models;
using dotnet_vsr.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_vsr.Controllers
{
    public class AccountController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var account = await db.Accounts.FirstOrDefaultAsync(acc => acc.Username == model.Username);
                if(account == null)
                {
                    ModelState.AddModelError(string.Empty, "The Username or Password is invalid, please try again!");
                }
                else
                {
                    // user exists, check if Passwords do match
                    if(account.Password == model.Password)
                    {
                        ModelState.AddModelError(string.Empty, "The Username or Password is invalid, please try again!");
                    }
                    else
                    {
                        // Username and password is correct
                        // set login cookie
                        Response.Cookies.Append("user", account.Username);
                        return RedirectToAction("Index","Home");
                    }
                }
            }
        
            return View(model);
        }

        public IActionResult LogOut()
        {
            Response.Cookies.Append("user", "");
            return RedirectToAction("Index" , "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                // check if user exists
                var acc = await db.Accounts.FirstOrDefaultAsync(ac => ac.Username == model.Username);
                if(acc == null)
                {
                    if(model.Password != model.Password2)
                    {
                        ModelState.AddModelError(string.Empty, "The password's do not match!");
                    }
                    else
                    {
                        Account newAccount = new Account {
                            Username = model.Username,
                            Password = model.Password
                        };

                        db.Accounts.Add(newAccount);
                        await db.SaveChangesAsync();

                        // login user after register is succesfull
                        Response.Cookies.Append("user", newAccount.Username);
                        return RedirectToAction("Index","Home");  
                    }                                   
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "This Username is already taken");
                }

            }
            return View(model);
        }
    }
}
