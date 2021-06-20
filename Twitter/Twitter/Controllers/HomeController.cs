using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Data;
using Twitter.Models;

namespace Twitter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext context)
        {
            _db = context;
        }
/*        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/
        public IActionResult Index()
        {
            var Tweets = _db.Tweets.ToList();
            ViewData["Tweets"] = Tweets;
            return View();
        }
        //Get  Home/TweeT
        public IActionResult Tweet()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Tweet([Bind( "Content", "Time", "ParentId", "UserId")] TweetModel Tweets)
        {
            if (ModelState.IsValid) //check the state of model
            {
                _db.Tweets.Add(Tweets);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        // POST - /Home/delete/id
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var Tweets = _db.Tweets.ToList().FirstOrDefault(p => p.Id == id);
            if (id == null || Tweets == null)
            {
                return View("_NotFound");
            }
            _db.Tweets.Remove(Tweets);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Get  Home/TweeT
        public IActionResult Reply()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Reply([Bind("Content", "Time", "ParentId", "UserId")] TweetModel Tweets)
        {
            if (ModelState.IsValid) //check the state of model
            {
                _db.Tweets.Add(Tweets);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Details(int? id)
        {
            var Tweets = _db.Tweets.ToList().Find(a => a.Id == id);
            ViewData["Tweet"] = Tweets;
            if (Tweets == null)
            {
                return Content("Not found");
            }
            else
            {
                ViewData["Tweet"] = Tweets;
                return View();
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
