using Microsoft.AspNetCore.Authorization;
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
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        [ActivatorUtilitiesConstructor]
        public ProfileController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Index(string id)
        {
            if(id==null)
            {
                id = _userManager.GetUserId(User);
            }
            
            var user = _db.Users.FirstOrDefault(user => user.Id == id);
            var Tweets = _db.Tweets.Where(t => t.UserId == id).ToList();
            var UserId = _userManager.GetUserId(User);
            if (UserId == null) ViewData["UserId"] = "none";
            else ViewData["UserId"] = UserId;
            Tweets.Reverse();
            ViewData["User"] = user;
            ViewData["Tweets"] = Tweets;
            return View();
        }

        public IActionResult Likes(string id)
        {
            var user = _db.Users.FirstOrDefault(user => user.Id == id);
            var UserId = _userManager.GetUserId(User);
            if (UserId == null) ViewData["UserId"] = "none";
            else ViewData["UserId"] = UserId;
            ViewData["User"] = user;
            var Likes = _db.Favorites.Where(f => f.UserId == id)
              .Include(f => f.Tweet).ThenInclude(t=>t.User).ToList();
            Likes.Reverse();
            ViewData["Likes"] = Likes;
            return View();
        }
    }
}
