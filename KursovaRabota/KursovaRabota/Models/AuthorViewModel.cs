using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.ComponentModel.DataAnnotations;

namespace KursovaRabota.Models
{
    public class AuthorViewModel
    {
        #region Properties
        public int ID { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }
        #endregion
#region Constructors
        public AuthorViewModel()
        {
            // create a default constructor, because the MVC needs it when the form is submitted, 
            // in order to create object of this type as parameter in an action
        }
        public AuthorViewModel(Author author)
        {
            this.Name = author.AuthorName;
            this.ID = author.AuthorID;
        }
        #endregion
        
    }
}