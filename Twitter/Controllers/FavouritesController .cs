using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Data;
using Twitter.Models;

namespace Twitter.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private object p;
        [ActivatorUtilitiesConstructor]
        public FavouritesController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public FavouritesController()
        {
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var favList = _db.Favorites.Where(f => f.UserId == userId)
              .Include(f => f.Tweet).ToList();
            var favEvents = new List<TweetModel>();
            foreach (var fav in favList)
                favEvents.Add(fav.Tweet);
            ViewData["favEvents"] = favEvents;
            return View();
        }
        // POST: /Events/delete/id
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var userId = _userManager.GetUserId(User);
            var Favorits = _db.Favorites.First(f => f.TweetId == id && f.UserId == userId);
            if (id == null || Favorits == null)
            {
                return View("_NotFound");
            }
            _db.Favorites.Remove(Favorits);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
