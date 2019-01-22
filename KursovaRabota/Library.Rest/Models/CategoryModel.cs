using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Rest.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public CategoryModel()
        { }

        public CategoryModel(Category category)
        {
            this.ID = category.CategoryID;
            this.Name = category.CateoryName;
        }

        public void CopyValuesToEntity(Category dbCategory)
        {
            dbCategory.CategoryID = this.ID;
            dbCategory.CateoryName = this.Name;
        }
    }


}