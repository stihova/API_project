using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.SOAP.Models
{
    public class AllBooksModel
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public int AuthorID { get; set; }
        public System.DateTime IssueDate { get; set; }
        public int CategoryID { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }

        public string ErrorMessage { get; set; }

        public AllBooksModel()
        {

        }

        public AllBooksModel(Book book)
        {
            this.BookID = book.BookID;
            this.BookName = book.BookName;
            this.AuthorID = book.AuthorID;
            this.IssueDate = book.IssueDate;
            this.CategoryID = book.CategoryID;
            this.Price = book.Price;
            this.Description = book.Description;
            this.DateCreated = book.DateCreated;
        }
    }
}