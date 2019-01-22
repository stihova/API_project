using DataAccess;
using Library.SOAP.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Library.SOAP
{
    /// <summary>
    /// Summary description for Books
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Books : System.Web.Services.WebService
    {

        [WebMethod]
        public List<AllBooksModel> GetAllBooks()
        {

            BookRepository bookRepository = new BookRepository();

            List<AllBooksModel> result = bookRepository.GetAll()
                .Select(b => new AllBooksModel(b))
                .ToList();

            return result;
        }

        [WebMethod]
        public AllBooksModel GetBookByID(int bookID)
        {
            try
            {
                BookRepository bookRepository = new BookRepository();

                Book dbBook = bookRepository.GetByID(bookID);
                if (dbBook == null)
                    throw new Exception($"Could not find a book with the given ID: {bookID}"); // it is better if you throw your custom Exception!

                AllBooksModel result = new AllBooksModel(dbBook);
                return result;
            }
            catch (Exception ex)
            {
                AllBooksModel result = new AllBooksModel();
                result.ErrorMessage = ex.GetBaseException().Message;
                return result;
            }
        }

        [WebMethod]
        public string AddBook(BookAddModel book)
        {
            try
            {
                BookRepository bookRepository = new BookRepository();

                Book newBook = new Book();
                newBook.BookName = book.BookName;
                newBook.AuthorID = book.AuthorID;
                newBook.IssueDate = book.IssueDate;
                newBook.CategoryID = book.CategoryID;
                newBook.Price = book.Price;
                newBook.Description = book.Description;
                newBook.DateCreated = book.DateCreated;
                bookRepository.Create(newBook);

                return "Book is added successfully";
            }
            catch (Exception ex)
            {
                return $"Failed to add the book. Error: {ex.GetBaseException().Message}";
            }
        }

        [WebMethod]
        public string EditBook2(BookUpdateModel book)
        {
            try
            {
                BookRepository bookRepository = new BookRepository();


                Book dbBook = bookRepository.GetByID(book.BookID);
                if (dbBook == null)
                    return $"Could not find a book with the given ID: {book.BookID}";

                dbBook.BookName = book.BookName;
                dbBook.AuthorID = book.AuthorID;
                dbBook.IssueDate = book.IssueDate;
                dbBook.CategoryID = book.CategoryID;
                dbBook.Price = book.Price;
                dbBook.Description = book.Description;
                dbBook.DateCreated = book.DateCreated;
                bookRepository.Update(dbBook, b => b.BookID == dbBook.BookID);



                return "Book is saved successfully";
            }
            catch (Exception ex)
            {
                return $"Failed to save the book. Error: {ex.GetBaseException().Message}";
            }
        }


        [WebMethod]
        public string DeleteBook(int bookID)
        {
            try
            {
                BookRepository bookRepository = new BookRepository();
                bookRepository.DeleteByID(bookID);
                return "Book is deleted successfully";
            }
            catch (Exception ex)
            {
                return $"Failed to delete book with id={bookID}. Error: {ex.GetBaseException().Message}";
            }
        }
    }
}

