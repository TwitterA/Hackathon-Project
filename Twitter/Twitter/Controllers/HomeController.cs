using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _db = context;
            _userManager = userManager;
        }
   
        public IActionResult Index()
        {
            var Tweets = _db.Tweets.Include(b => b.User).ToList();
            Tweets.Reverse();
            var UserId = _userManager.GetUserId(User);
            if(UserId==null) ViewData["UserId"] = "none";
            else ViewData["UserId"] = UserId;
            ViewData["Tweets"] = Tweets;
            
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Tweet([Bind("Content")] TweetModel Tweet, string UserId)
        {
            Tweet.UserId = UserId;
            Tweet.Time = DateTime.Now.ToString();
            if (ModelState.IsValid) //check the state of model
            {
                _db.Tweets.Add(Tweet);
                _db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        // POST - /Home/delete/id
        [Authorize]
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var Tweet = _db.Tweets.ToList().FirstOrDefault(p => p.Id == id);
            if (id == null || Tweet == null)
            {
                return View("_NotFound");
            }
            _db.Tweets.Remove(Tweet);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Reply([Bind("Content")] TweetModel Tweet, int ParentId, string UserId)
        {
            Tweet.UserId = UserId;
            Tweet.Time = DateTime.Now.ToString();
            Tweet.ParentId = ParentId;
            if (ModelState.IsValid) //check the state of model
            {
                _db.Tweets.Add(Tweet);
                _db.SaveChanges();
                return RedirectToAction("Details", "Home", new { Id=ParentId });
            }
            return View();
        }
        public IActionResult Details(int? id)
        {
            var Tweet = _db.Tweets.Include(t => t.User).Include(t => t.Replies).ToList().Find(t => t.Id == id);

            if (Tweet == null)
            {
                return Content("Not found");
            }
            var UserId = _userManager.GetUserId(User);
            if (UserId == null) ViewData["UserId"] = "none";
            else ViewData["UserId"] = UserId;
            ViewData["Tweet"] = Tweet;
            return View();

        }

        [Authorize]
        public IActionResult Like(int id)
        {
            var Tweet = _db.Tweets.Include(t => t.User).Include(t => t.Replies).ToList().Find(t => t.Id == id);

            if (Tweet == null)
            {
                return Content("Not found");
            }
            FavoriteModel fave = new FavoriteModel();
            
            var UserId = _userManager.GetUserId(User);
            
            fave.UserId = UserId;
            fave.TweetId = id;
            _db.Favorites.Add(fave);
            _db.SaveChanges();
            return RedirectToAction("Index");

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
