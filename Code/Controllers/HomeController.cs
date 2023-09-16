using Azure.Identity;
using Code;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace Code.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Logout()
        {
            // Set the message in TempData
            TempData["Message"] = "Logged out successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult LogIn(int check = 0)
        {
            ViewBag.Check = false;
            if (check == 0)
                ViewBag.check = false;
            else
                ViewBag.check = true;
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        [HttpPost]
        public IActionResult UserForm(string Name, string Email, string Password)
        {
            User u = new User();
            u.Name = Name;
            u.Email = Email;
            u.Password = Password;
            bool isFlag = UserRepository.AddUser(u);
            if (isFlag)
            {
                TempData["x"] = "Email Already Taken, Try Again";
                var s = TempData["x"];
                ViewBag.check = false;
                return View("LogIn", s);
            }
            else
                return RedirectToAction("LogIn", "Home", new {check=1});
        }

        [HttpPost]
        public IActionResult AuthorizeLogIn(User u)
        {
            if (UserRepository.AuthorizeUser(u))
            {
                string name = UserRepository.returnName(u);
                int id = UserRepository.returnId(u);
                if (!HttpContext.Session.Keys.Contains("Username"))
                {
                    HttpContext.Session.SetString("Username", name);
                    HttpContext.Session.SetInt32("UserId", id);
                }

                return RedirectToAction("Welcome", "UserInterface", new {check = 1});
            }
            else
            {
                TempData["ErrorMessage"] = "Incorrect Email or Password.";
                return RedirectToAction("LogIn");
            }
        }

    }
}