using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using KursovaRabota.Helpers;
using System.IO;

namespace KursovaRabota.Models
{
    public class SearchViewModel
    {
        #region Properties
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string BookDescription { get; set; }
        public string BookImageUrl { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }

        public bool HasImage { get; set; }
        #endregion

        #region Constructors
        public SearchViewModel(Book dbBooks)
        {
            this.BookID = dbBooks.BookID;
            this.BookName = dbBooks.BookName;
            this.BookDescription = dbBooks.Description;

            this.AuthorName = dbBooks.Author.AuthorName;

            this.CategoryName = dbBooks.Category.CateoryName;

        }
        #endregion
    }
}