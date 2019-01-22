using DataAccess;
using KursovaRabota.Helpers;
using KursovaRabota.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KursovaRabota.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.Message = TempData["Message"];

            BookRepository bookRepository = new BookRepository();
            List<Book> allBooks = bookRepository.GetAll();

            List<BookViewModel> model = new List<BookViewModel>();
            foreach (Book dbBooks in allBooks)
            {
                BookViewModel bookViewModel = new BookViewModel(dbBooks);
                model.Add(bookViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            // add the Books to the Viewbag
            AuthorRepository authorRepository = new AuthorRepository();
            List<Author> allAuthors = authorRepository.GetAll();
            ViewBag.allAuthors = new SelectList(allAuthors, "ID", "Name");

            // add the Books to the Viewbag
            CategoryRepository categoryRepository = new CategoryRepository();
            List<Category> allCategories = categoryRepository.GetAll();
            ViewBag.AllCategories = new SelectList(allCategories, "ID", "Name");

            // create the viewmodel, based on the Book from the database
            BookViewModel model = new BookViewModel();
            BookRepository bookRepository = new BookRepository();
            Book dbBooks = bookRepository.GetByID(id);
            if (dbBooks != null)
            {
                model = new BookViewModel(dbBooks);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel viewModel)
        {
            if (viewModel == null)
            {
                // this should not be possible, but just in case, validate
                TempData["ErrorMessage"] = "Ups, a serious error occured: No Viewmodel.";
                return RedirectToAction("Index");
            }

            // get the item from the database by ID
            BookRepository bookRepository = new BookRepository();
            Book dbBooks = bookRepository.GetByID(viewModel.ID);

            // if there is no object in the DB, this is a new item -> will create a new one
            if (dbBooks == null)
            {
                dbBooks = new Book();
                dbBooks.DateCreated = DateTime.Now;
            }

            // update the DB object from the viewModel 
            dbBooks.CategoryID = viewModel.CategoryID;
            dbBooks.AuthorID = viewModel.AuthorID;
            dbBooks.BookName = viewModel.BookName;
            dbBooks.Description = viewModel.Description;

            bookRepository.Save(dbBooks);

            TempData["Message"] = "The book was saved successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            BookRepository bookRepository = new BookRepository();
            bool isDeleted = bookRepository.DeleteByID(id);

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a book with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The book was deleted successfully";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            BookRepository bookRepository = new BookRepository();

            // get the DB object
            Book dbBooks = bookRepository.GetByID(id);
            if (dbBooks == null)
            {
                // when we have RedirectToAction, we can not use Viewbag - so we use a TempData!
                TempData["ErrorMessage"] = "Could not find a book with ID = " + id;
                return RedirectToAction("Index");
            }
            else
            {
                // create the view model
                BookViewModel model = new BookViewModel(dbBooks);
                return View(model);
            }
        }
    }
}