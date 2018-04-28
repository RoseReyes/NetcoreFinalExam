using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using auctions.Models;

namespace auctions.Controllers
{
    public class HomeController : Controller
    {
        
        private AuctionContext _context;

        public HomeController(AuctionContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [Route("Register")]
         public IActionResult register(RegisterViewModel registerVM)
        { 
            if(ModelState.IsValid)
            {
                
                User user = new User
                {
                    Name = registerVM.Name,
                    Alias = registerVM.Alias,
                    Email = registerVM.Email,
                    Password = registerVM.Password,
                };

                //Hashed Password
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);

                //Save to DB
                _context.Add(user);
                _context.SaveChanges();

                //set userid into session
                HttpContext.Session.SetInt32("user_id", user.UserId);
                return RedirectToAction("Home");
            }
            return View("Index");
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult login(string email, string password)
        {   
            List<User> ReturnedValues = _context.Users.Where(s => s.Email.Equals(email)).ToList();
            
            if(ModelState.IsValid)
            {
                    foreach (var users in ReturnedValues)
                    {                    
                       if(users.Email != null && email != null)
                        {  
                            var Hasher = new PasswordHasher<User>();
                            // Pass the user object, the hashed password, and the PasswordToCheck
                            if(0 != Hasher.VerifyHashedPassword(users, users.Password, password))
                            {
                                //Handle success and Set ID and Name to session
                                HttpContext.Session.SetInt32("user_id", users.UserId);
                                return RedirectToAction("Home");
                            }
                        }
                        TempData["logerror"] = "Invalid email or password. Check your email or register!";
                        return View("Index");  
                    }
             };
            TempData["logerror"] = "Invalid email or password. Check your email or register!";
            return View("Index");
        }
            
        
        [HttpGet]
        [Route("Home")]
        public IActionResult Home() 
        {
            int? userid = HttpContext.Session.GetInt32("user_id");
            ViewBag.Id = userid;

            //Check session if null
            if(userid != null)
            {
                 //get joining query
                 var ReturnedValues = _context.Posts.Include(r => r.User).Include(u => u.likes).ThenInclude( m => m.User).ToList();
                 ViewBag.display = ReturnedValues;
                 //get username
                 ViewBag.Username = _context.Users.SingleOrDefault(u => u.UserId == userid);
            }
            else {
                 return RedirectToAction("Index");
            }
            return View("AuctionBoard");

        }

        [HttpGet]
        [Route("users/{UserId}")]
        public IActionResult users(int UserId)
        {
            List<Post> ReturnedValues = _context.Posts.Where(r => r.UserId == UserId).Include( w=> w.User).Include(u => u.likes).ThenInclude( m => m.User).ToList(); 
            ViewBag.Result = ReturnedValues;
            return View("NewAuction");
        }

        [HttpGet]
        [Route("bright_ideas/{PostId}")]
        public IActionResult bright_ideas(int PostId)
        {
             List<Post> ReturnedValues = _context.Posts.Where(r => r.PostId == PostId).Include( w=> w.User).Include(u => u.likes).ThenInclude( m => m.User).ToList();
             ViewBag.Result = ReturnedValues;
            return View("BidArea");
        }

        [HttpPost]
        [Route("Idea")]
        public IActionResult Idea(string postmsg) 
        {
            int? userid = HttpContext.Session.GetInt32("user_id");
            ViewBag.Id = userid;

             Post newpost = new Post
                {
                    UserId = (int) HttpContext.Session.GetInt32("user_id"),
                    Idea = postmsg,
                };

                //Save to DB
                _context.Add(newpost);
                _context.SaveChanges();
        
           return RedirectToAction("Home");
        }

        [HttpPost]
        [Route("LikedPost")]
        public IActionResult LikedPost(int postid)
        {
            Like newlike = new Like
                {
                    UserId = (int) HttpContext.Session.GetInt32("user_id"),
                    PostId = postid,
                };
                //Save to DB
                _context.Add(newlike);
                _context.SaveChanges();
            return RedirectToAction("Home");
        }

        
        [HttpPost]
        [Route("delete")]
        public IActionResult delete(int postid)
        {
            Post deletepost = _context.Posts.FirstOrDefault(a => a.PostId == postid);
            _context.Posts.Remove(deletepost);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }
    
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return Redirect("/");
         }


    }
}
