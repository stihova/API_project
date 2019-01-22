using DataAccess;
using KursovaRabota.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KursovaRabota.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.Message = TempData["Message"];

            // 1. вземаме всички автори от DB
            AuthorRepository authorRepository = new AuthorRepository();
            List<Author> allAuthors = authorRepository.GetAll();

            // инициализираме модела от View
            List<AuthorViewModel> model = new List<AuthorViewModel>();

            // 2. конвертираме всички Author objects във ViewModel objects
            foreach (Author author in allAuthors)
            {
                AuthorViewModel newItem = new AuthorViewModel(author);
                model.Add(newItem);
            }

            // 3. pass the viewModel to the view
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            // get the Author to edit
            AuthorRepository authorRepository = new AuthorRepository();
            Author author = authorRepository.GetByID(id);

            AuthorViewModel model = new AuthorViewModel();
            if (author != null)
            {
                // create the viewModel from the Author
                model = new AuthorViewModel(author);

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AuthorViewModel authorEdit)
        {
            // find the Author in the DB
            AuthorRepository authorRepository = new AuthorRepository();
            Author dbAuthor = authorRepository.GetByID(authorEdit.ID);

            // if there is no object in the DB, this is a new item -> will create a new one
            if (dbAuthor == null)
            {
                dbAuthor = new Author();
            }

            // update the DB object from the viewModel 
            dbAuthor.AuthorName = authorEdit.Name;
            authorRepository.Save(dbAuthor);

            TempData["Message"] = "The author was saved successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            bool isDeleted = authorRepository.DeleteByID(id);

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a author with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The record was deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}