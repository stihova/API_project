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
    [RoutePrefix("api/authors")]
    /// <summary>
    /// AuthorController: Get, Put, Post, Delete methods
    /// </summary>
    public class AuthorController : ApiController
    {
            [HttpGet]
            [Route]
        /// <summary>
        /// Get method - returns all authors
        /// </summary>
        public List<AuthorModel> Get()
            {
                AuthorRepository authorRepository = new AuthorRepository();
                var allAuthor = authorRepository.GetAll()
                    .Select(c => new AuthorModel(c))
                    .ToList();
                return allAuthor;
            }

            [HttpGet]
            [Route("{authorID:int}")]
        /// <summary>
        /// Get element(author) by ID
        /// </summary>
        public IHttpActionResult GetByID(int? authorID)
            {
                if (authorID == null)
                    return BadRequest("the parameter authorID is empty");

                AuthorRepository authorRepository = new AuthorRepository();
                Author author = authorRepository.GetByID(authorID.Value);
                if (author == null)
                    return BadRequest($"Could not find author with ID: {authorID}");

                AuthorModel apiAuthor = new AuthorModel(author);
                return Ok(apiAuthor);
            }

            [HttpGet]
            [Route("search")]
        /// <summary>
        /// Get element(author) by name
        /// </summary>
        public IHttpActionResult GetAuthorName(string authorName = null)
            {

                AuthorRepository autrohRepository = new AuthorRepository();
                List<Author> author = autrohRepository.GetAuthorByName(authorName);

                List<AuthorModel> apiAuthors = author
                    .Select(b => new AuthorModel(b))
                    .ToList();

                return Ok(apiAuthors);
            }

            [HttpPut]
            [Route]
            public IHttpActionResult Put(AuthorModel author)
            {
                try
                {
                    AuthorRepository authorRepository = new AuthorRepository();
                    Author dbAuthor = authorRepository.GetByID(author.ID);
                    if (dbAuthor == null)
                        return NotFound();

                    author.CopyValuesToEntity(dbAuthor);
                    authorRepository.Update(dbAuthor, x => x.AuthorID == dbAuthor.AuthorID);

                    return StatusCode(HttpStatusCode.NoContent);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPost]
            [Route]
        /// <summary>
        /// Post method
        /// </summary>
        public IHttpActionResult Post(AuthorModel author)
            {
                try
                {
                    AuthorRepository authorRepository = new AuthorRepository();
                    Author dbAuthor = new Author();


                    author.CopyValuesToEntity(dbAuthor);
                    authorRepository.Create(dbAuthor);

                    // return the newly created Author
                    AuthorModel newAuthor = new AuthorModel(dbAuthor);
                    return Ok(newAuthor);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpDelete]
            [Route("{authorID:int}")]
        /// <summary>
        /// Delete method- delete author with selected ID
        /// </summary>
        public IHttpActionResult Delete(int authorID)
            {
                try
                {
                    AuthorRepository authorRepository = new AuthorRepository();
                    Author dbAuthor = authorRepository.GetByID(authorID);
                    if (dbAuthor == null)
                        return NotFound();

                    authorRepository.DeleteByID(authorID);

                    return StatusCode(HttpStatusCode.NoContent);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
}