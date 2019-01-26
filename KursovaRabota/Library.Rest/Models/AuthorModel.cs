using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Rest.Models
{
    public class AuthorModel
    {
        //Свойства на AuthorModel
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public AuthorModel()
        {

        }

        public AuthorModel(Author author)
        {
            this.ID = author.AuthorID;
            this.Name = author.AuthorName;
            this.Country = author.Country;
        }

        public void CopyValuesToEntity(Author dbAuthor)
        {
            dbAuthor.AuthorID = this.ID;
            dbAuthor.AuthorName = this.Name;
            dbAuthor.Country = this.Country;
        }
    }
}