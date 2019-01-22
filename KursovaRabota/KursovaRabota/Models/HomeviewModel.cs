using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using KursovaRabota.Helpers;
using System.IO;

namespace KursovaRabota.Models
{
    public class HomeBookViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class HomeViewModel
    {
        #region Properties
        public List<HomeBookViewModel> BooksList { get; set; }
        #endregion

        #region Constructors
        public HomeViewModel()
        {
            // initialize the list with Images; it should not be null
            BooksList = new List<HomeBookViewModel>();
        }

        public HomeViewModel(List<Book> allBooks)
            : this()
        {
            foreach (Book book in allBooks)
            {
                
                    HomeBookViewModel item = new HomeBookViewModel();
                item.Name = book.BookName;
                    item.ID = book.BookID;
                    BooksList.Add(item);
                
            }
        }
        #endregion
    }
}