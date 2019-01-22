//using BookLibrary.Business;
using BookLibrary.Rpc.Models;
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
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        /// <summary>
        /// Sum of two numbers in octal numeric system
        /// </summary>
        /// <param name="number1">First number</param>
        /// <param name="number2">Second number</param>
        /// <returns>The sum of the two numbers</returns>
        
        /// <summary>
        /// Sum of two numbers in octal numeric system
        /// </summary>

        [HttpGet]
        [Route]
        public List<BookModel> Get()
        {
            BookRepository bookRepository = new BookRepository();
            var allBooks = bookRepository.GetAll()
                .Select(c => new BookModel(c))
                .ToList();
            return allBooks;
        }

        [HttpGet]
        [Route("{bookID:int}")]
        public IHttpActionResult GetByID(int? bookID)
        {
            if (bookID == null)
                return BadRequest("the parameter bookID is empty");

            BookRepository bookRepository = new BookRepository();
            Book book = bookRepository.GetByID(bookID.Value);
            if (book == null)
                return BadRequest($"Could not find book with ID: {bookID}");

            BookModel apiBook = new BookModel(book);
            return Ok(apiBook);
        }


        [HttpGet]
        [Route("search")]
        public IHttpActionResult GetBookName(string bookName = null)
        {

            BookRepository bookRepository = new BookRepository();
            List<Book> books = bookRepository.GetBookByName(bookName);

            List<BookModel> apiBooks = books
                .Select(b => new BookModel(b))
                .ToList();

            return Ok(apiBooks);
        }

        [HttpPut]
        [Route]
        public IHttpActionResult Put(BookModel book)
        {
            try
            {
                BookRepository bookRepository = new BookRepository();
                Book dbBook = bookRepository.GetByID(book.BookID);
                if (dbBook == null)
                    return NotFound();

                book.CopyValuesToEntity(dbBook);
                bookRepository.Update(dbBook, x => x.BookID == dbBook.BookID);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route]
        public IHttpActionResult Post(BookModel book)
        {
            try
            {
                BookRepository bookRepository = new BookRepository();
                Book dbBook = new Book();


                book.CopyValuesToEntity(dbBook);
                bookRepository.Create(dbBook);

                // return the newly created Book
                BookModel newBook = new BookModel(dbBook);
                return Ok(newBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{bookID:int}")]
        public IHttpActionResult Delete(int bookID)
        {
            try
            {
                BookRepository bookRepository = new BookRepository();
                Book dbBook = bookRepository.GetByID(bookID);
                if (dbBook == null)
                    return NotFound();

                bookRepository.DeleteByID(bookID);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}