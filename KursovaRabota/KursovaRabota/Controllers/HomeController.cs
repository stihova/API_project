using DataAccess;
using KursovaRabota.Helpers;
using KursovaRabota.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KursovaRabota.Controllers
{

    // LogIn and Register
    #region mockup classes
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public bool IsAdministrator { get; set; }
    }

    public class UserRepository
    {
        public User GetUserByNameAndPassword(string username, string password)
        {
            return new User() { ID = 2, Username = username, IsAdministrator = true };
        }
    }
    #endregion


    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            BookRepository bookRepository = new BookRepository();
            List<Book> allBooks = bookRepository.GetAll();

            HomeViewModel model = new HomeViewModel(allBooks);
            return View(model);
        }

        public ActionResult Search(string searchVal)
        {
            BookRepository bookRepository = new BookRepository();

            // за превенция - NullPointerException
            if (searchVal == null)
            {
                searchVal = string.Empty;
            }

            searchVal = searchVal.ToLower();

            //  filter the matched objects
            List<Book> foundBooks = bookRepository.GetAll()
                .Where(c => c.BookName.ToLower().Contains(searchVal)
                    || c.Description.ToLower().Contains(searchVal))
                .ToList();

            // конвертираме DB objects във ViewModel objects
            List<SearchViewModel> model = new List<SearchViewModel>();
            foreach (Book dbBooks in foundBooks)
            {
                SearchViewModel modelItem = new SearchViewModel(dbBooks);
                model.Add(modelItem);
            }

            return View(model);
        }

     //LogIn

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult LoginPost(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // проверяваме дали съществува в DB
                UserRepository userRepository = new UserRepository();
                User dbUser = userRepository.GetUserByNameAndPassword(viewModel.Username, viewModel.Password);

                bool isUserExists = dbUser != null;
                if (isUserExists)
                {
                    LoginUserSession.Current.SetCurrentUser(dbUser.ID, dbUser.Username, dbUser.IsAdministrator);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and/or password");
                }
            }

            // до тук се стига ако имаме validation error
            return View();
        }

        public ActionResult Logout()
        {
            LoginUserSession.Current.Logout();
            return RedirectToAction("Index");
        }

        //Register

        public ActionResult Index2()
        {
            // добавяме Message във Viewbag
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            // валидация за Firstname да започва с capital letter
            if (string.IsNullOrEmpty(viewModel.FirstName) == false)
            {
                char firstLetter = viewModel.FirstName[0];
                if (char.IsUpper(firstLetter) == false)
                {
                    ModelState.AddModelError("FirstName", "First name should start with a capital letter!");
                }
            }

            // сървър валидация за banned users
            if (viewModel.FirstName == "Ivan" && viewModel.LastName == "Ivanov")
            {
                ModelState.AddModelError("", "Sorry, this user is banned from our web server!");
            }

            if (ModelState.IsValid)
            {
                TempData["Message"] = "You registered succesfully!";
                return RedirectToAction("Index2");
            }
            else
            {
                return View();
            }
        }

        public JsonResult ValidateEmail(string email)
        {
            bool isEmailUsed = false;
            if (string.IsNullOrEmpty(email) == false)
            {
                isEmailUsed = (email == "slavi0705@abv.bg") || (email == "slavena0tihova@gmail.com");
            }
            return Json(!isEmailUsed, JsonRequestBehavior.AllowGet);
        }
    }
}