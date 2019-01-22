using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.ComponentModel.DataAnnotations;

namespace KursovaRabota.Models
{
    public class CategoryViewModel
    {
        #region Properties
        public int ID { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }
        #endregion

        #region Constructors
        public CategoryViewModel()
        {
            // create a default constructor, because the MVC needs it when the form is submitted, 
            // in order to create object of this type as parameter in an action
        }
        public CategoryViewModel(Category category)
        {
            this.Name = category.CateoryName;
            this.ID = category.CategoryID;
        }
        #endregion
    }
}