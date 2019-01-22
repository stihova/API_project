using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.SOAP.Models
{
    public class BookAddModel
    {
       // public int BookID { get; set; }
        public string BookName { get; set; }
        public int AuthorID { get; set; }
        public System.DateTime IssueDate { get; set; }
        public int CategoryID { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    }
}