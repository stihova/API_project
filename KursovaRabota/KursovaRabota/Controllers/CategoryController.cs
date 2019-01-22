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
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.Message = TempData["Message"];

            // 1. вземаме всички categories от DB
            CategoryRepository categoryRepository = new CategoryRepository();
            List<Category> allCategories = categoryRepository.GetAll();

            // инициализираме модела от View
            List<CategoryViewModel> model = new List<CategoryViewModel>();

            // 2. конвертираме всички Category objects във ViewModel objects
            foreach (Category category in allCategories)
            {
                CategoryViewModel newItem = new CategoryViewModel(category);
                model.Add(newItem);
            }

            // 3. pass the viewModel to the view
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            // get the Category to edit
            CategoryRepository categoryRepository = new CategoryRepository();
            Category category = categoryRepository.GetByID(id);

            CategoryViewModel model = new CategoryViewModel();
            if (category != null)
            {
                // create the viewModel from the Category
                model = new CategoryViewModel(category);

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryEdit)
        {
            // find the Category in the DB
            CategoryRepository categoryRepository = new CategoryRepository();
            Category dbCategory = categoryRepository.GetByID(categoryEdit.ID);

            // if there is no object in the DB, this is a new item -> will create a new one
            if (dbCategory == null)
            {
                dbCategory = new Category();
            }

            // update the DB object from the viewModel 
            dbCategory.CateoryName = categoryEdit.Name;
            // no need to update the ID: dbCategory.ID = categoryEdit.ID;
            categoryRepository.Save(dbCategory);

            TempData["Message"] = "The category was saved successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            bool isDeleted = categoryRepository.DeleteByID(id);

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a category with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The record was deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}