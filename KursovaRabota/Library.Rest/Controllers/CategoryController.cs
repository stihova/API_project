using DataAccess;
using Library.Rest.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.Rest.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoryController : ApiController
    {
        [HttpGet]
        [Route]
        public List<CategoryModel> Get()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            var allCategories = categoryRepository.GetAll()
                .Select(c => new CategoryModel(c))
                .ToList();
            return allCategories;
        }

        [HttpGet]
        [Route("{categoryID:int}")]
        public IHttpActionResult GetByID(int? categoryID)
        {
            if (categoryID == null)
                return BadRequest("the parameter categoryID is empty");

            CategoryRepository categoryRepository = new CategoryRepository();
            Category category = categoryRepository.GetByID(categoryID.Value);
            if (category == null)
                return BadRequest($"Could not find category with ID: {categoryID}");

            CategoryModel apiCategory = new CategoryModel(category);
            return Ok(apiCategory);
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult GetCategoryName(string categoryName = null)
        {

            CategoryRepository categoryRepository = new CategoryRepository();
            List<Category> category = categoryRepository.GetCategoryByName(categoryName);

            List<CategoryModel> apiCategory = category
                .Select(b => new CategoryModel(b))
                .ToList();

            return Ok(apiCategory);
        }

        [HttpPut]
        [Route]
        public IHttpActionResult Put(CategoryModel category)
        {
            try
            {
                CategoryRepository categoryRepository = new CategoryRepository();
                Category dbCategory = categoryRepository.GetByID(category.ID);
                if (dbCategory == null)
                    return NotFound();

                category.CopyValuesToEntity(dbCategory);
                categoryRepository.Update(dbCategory, x => x.CategoryID == dbCategory.CategoryID);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route]
        public IHttpActionResult Post(CategoryModel category)
        {
            try
            {
                CategoryRepository categoryRepository = new CategoryRepository();
                Category dbCategory = new Category();


                category.CopyValuesToEntity(dbCategory);
                categoryRepository.Create(dbCategory);

                // return the newly created Category
                CategoryModel newCategory = new CategoryModel(dbCategory);
                return Ok(newCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{categoryID:int}")]
        public IHttpActionResult Delete(int categoryID)
        {
            try
            {
                CategoryRepository categoryRepository = new CategoryRepository();
                Category dbCategory = categoryRepository.GetByID(categoryID);
                if (dbCategory == null)
                    return NotFound();

                categoryRepository.DeleteByID(categoryID);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}