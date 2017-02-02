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
                                        .Where(m => m.ParentMessage == null)
                                        .OrderByDescending(o => o.PostDate)
                                        .ToList();

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
        public IActionResult Like(int messageId)
        {
            var db = new DataAccess.DatabaseContext();
            string user = Request.Cookies["user"];
            var acc = db.Accounts.Include(a => a.Messages)
                .Include( a => a.Upvotes)
                .SingleOrDefault(a => a.Username == user);

            var upvote = new Upvote() { MessageId = messageId, AccountId = acc.ID };

            acc.Upvotes.Add(upvote);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Favorite(int messageId)
        {
            var db = new DataAccess.DatabaseContext();
            string user = Request.Cookies["user"];
            var acc = db.Accounts.Include(a => a.Messages)
                .Include(a => a.Favorites)
                .SingleOrDefault(a => a.Username == user);

            var favorite = new Favorite() { MessageId = messageId, AccountId = acc.ID };

            acc.Favorites.Add(favorite);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Unlike(int messageId)
        {
            // TODO
            return RedirectToAction("index");
        }
        public IActionResult Unfavorite(int messageId)
        {
            // TODO
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Index(string text)
        {
            // get logedin user
            string user = Request.Cookies["user"];
            var acc = db.Accounts.Include(a => a.Messages).SingleOrDefault(a => a.Username == user);

            // dont add message if account and text are empty
            if(text != "" && acc != null)
            {
                acc.Messages.Add(new Message {
                    Account = acc,
                    Text = text,
                    PostDate = DateTime.Now
                });
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }

        public ActionResult Details(int id)
        {
            // get message with message id
            Message post = db.Messages
                                .Include(m => m.Account)
                                .Include(m => m.Upvotes)
                                .Include(m => m.Favorites)
                                .SingleOrDefault(m => m.ID == id);

            if(post == null)
                return RedirectToAction("index");

            // get messages with this id as parent id
            List<Message> messages = db.Messages.Include(m => m.ParentMessage).Where(m => m.ParentMessage.ID == id).ToList();

            DetailsViewModel model = new DetailsViewModel {
                Message = post,
                Replies = messages
            };
            return View(model);
        }
        public ActionResult Likes()
        {
            string user = Request.Cookies["user"];
            var acc = db.Accounts.SingleOrDefault(a => a.Username == user);

            // get messages with no parent id, sort by desc
            List<Message> messages = db.Messages
                                        .Include(m => m.Account)
                                        .Include(m => m.Upvotes)
                                        .Include(m => m.Favorites)
                                        .Where(m => m.Upvotes.SingleOrDefault(up => up.AccountId == acc.ID) != null)
                                        .OrderByDescending(o => o.PostDate).ToList();

            IndexViewModel model = new IndexViewModel {
                Account = acc,
                Messages = messages
            };

            return View(model);
        }
        public ActionResult Favorites()
        {
            string user = Request.Cookies["user"];
            var acc = db.Accounts.SingleOrDefault(a => a.Username == user);

            // get messages with no parent id, sort by desc
            List<Message> messages = db.Messages
                                        .Include(m => m.Account)
                                        .Include(m => m.Upvotes)
                                        .Include(m => m.Favorites)
                                        .Where(m => m.Favorites.SingleOrDefault(up => up.AccountId == acc.ID) != null)
                                        .OrderByDescending(o => o.PostDate).ToList();

            IndexViewModel model = new IndexViewModel {
                Account = acc,
                Messages = messages
            };

            return View(model);
        }
    }
}
