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

    public class AuthorController : ApiController
    {
            [HttpGet]
            [Route]
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
            public IHttpActionResult Post(AuthorModel author)
            {
                try
                {
                    AuthorRepository authorRepository = new AuthorRepository();
                    Author dbAuthor = new Author();


                    author.CopyValuesToEntity(dbAuthor);
                    authorRepository.Create(dbAuthor);

                    // return the newly created Book
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