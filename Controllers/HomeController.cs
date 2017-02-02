using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet_core.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using dotnet_vsr.ViewModels;

namespace dotnet_vsr.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        public IActionResult Index()
        {
            // get logedin user
            string user = Request.Cookies["user"];
            var acc = db.Accounts.SingleOrDefault(a => a.Username == user);

            // get messages with no parent id, sort by desc
            List<Message> messages = db.Messages
                                        .Include(m => m.Account)
                                        .Include(m => m.Upvotes)
                                        .Include(m => m.Favorites)
                                        .Where(m => m.ParentMessage == null).OrderByDescending(o => o.ID).ToList();

            IndexViewModel model = new IndexViewModel {
                Account = acc,
                Messages = messages
            };

            return View(model);
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
        public IActionResult Like(int messageId, int accountId)
        {
            var c = new DataAccess.DatabaseContext();
            var upvote = new Upvote() { MessageId = messageId, AccountId = accountId };

            var a = c.Accounts.Include(x => x.Upvotes).Include(x => x.Messages).FirstOrDefault(x => x.ID == accountId);


            a.Upvotes.Add(upvote);
            ViewData["Account"] = a;  
            ViewData["Message"] = "Welcome";
            c.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Favorite(int messageId, int accountId)
        {
            var c = new DataAccess.DatabaseContext();
            var favorite = new Favorite() { MessageId = messageId, AccountId = accountId };

            var a = c.Accounts.Include(x => x.Favorites).Include(x => x.Messages).FirstOrDefault(x => x.ID == accountId);

            a.Favorites.Add(favorite);
            ViewData["Account"] = a;  
            ViewData["Message"] = "Welcome";
            c.SaveChanges();
            return RedirectToAction("index");            
        }

        [HttpPost]
        public ActionResult Index(string text)
        {
            // get logedin user
            string user = Request.Cookies["user"];
            var acc = db.Accounts.Include(a => a.Messages).SingleOrDefault(a => a.Username == user);

            // dont add message if account and text are empty
            if(text != null && acc != null)
            {
                acc.Messages.Add(new Message {
                    Account = acc,
                    Text = text
                });
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }

        public ActionResult Details(int id)
        {
            // get logedin user
            string user = Request.Cookies["user"];
            var acc = db.Accounts.Include(a => a.Messages).SingleOrDefault(a => a.Username == user);

            // get message with message id
            Message post = db.Messages
                                .Include(m => m.Account)
                                .Include(m => m.Upvotes)
                                .SingleOrDefault(m => m.ID == id);

            if(post == null)
                return RedirectToAction("index");

            // get messages with this id as parent id
            List<Message> messages = db.Messages
                                        .Include(m => m.ParentMessage)
                                        .Include(m => m.Account)
                                        .Include(m => m.Upvotes)
                                        .Include(m => m.Favorites)                                
                                        .Where(m => m.ParentMessage.ID == id).ToList();

            DetailsViewModel model = new DetailsViewModel {
                Account = acc,
                Message = post,
                Replies = messages
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Details(string text, int messageId)
        {
            // get logedin user
            string user = Request.Cookies["user"];
            var acc = db.Accounts.Include(a => a.Messages).SingleOrDefault(a => a.Username == user);

            // dont add message if account and text are empty
            if(text != null && acc != null)
            {
                acc.Messages.Add(new Message {
                    Account = acc,
                    Text = text,
                    ParentMessage = db.Messages.SingleOrDefault(m => m.ID == messageId)
                });
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = messageId });
        }
    }
}
