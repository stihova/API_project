//using BookLibrary.Entities;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibrary.Rpc.Models
{
    public class BookModel
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public int AuthorID { get; set; }
        public System.DateTime IssueDate { get; set; }
        public int CategoryID { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }

        public BookModel()
        { }
        public BookModel(Book book)
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

        public void CopyValuesToEntity(Book dbBook)
        {
            dbBook.BookID = this.BookID;
            dbBook.BookName = this.BookName;
            dbBook.AuthorID = this.AuthorID;
            dbBook.IssueDate = this.IssueDate;
            dbBook.CategoryID = this.CategoryID;
            dbBook.Price = this.Price;
            dbBook.Description = this.Description;
            dbBook.DateCreated = this.DateCreated;
        }
    }
}