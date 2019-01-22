using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.IO;
using KursovaRabota.Helpers;
using System.ComponentModel.DataAnnotations;

namespace KursovaRabota.Models
{
    public class BookViewModel
    {
        #region Properties
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int AuthorID { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BookName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }
        public string ImageName { get; set; }
        public System.DateTime? DateCreated { get; set; }
        public string Email { get; set; }


        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public bool HasImage { get; set; }
        public string ImagePath { get; set; }
        #endregion

        #region Constructors

        public BookViewModel()
        {
        }
        public BookViewModel(Book dbBooks)
        {
            this.CategoryID = dbBooks.CategoryID;
            this.CategoryName = dbBooks.Category.CateoryName;
            this.AuthorID = dbBooks.AuthorID;
            this.AuthorName = dbBooks.Author.AuthorName;
            this.BookName = dbBooks.BookName;
            this.DateCreated = dbBooks.DateCreated;
            this.Description = dbBooks.Description;
            //this.Email = dbBooks.Email;
            this.ID = dbBooks.BookID;
            //this.ImageName = dbBooks.ImageName;

            //this.HasImage = string.IsNullOrEmpty(dbBooks.ImageName) == false;
            //if (this.HasImage)
            //{
            //    this.ImagePath = Path.Combine(Constants.ImagesDirectory, this.ImageName);
            //}
        }
        #endregion
    }
}