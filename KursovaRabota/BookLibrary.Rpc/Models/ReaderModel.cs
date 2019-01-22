using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibrary.Rpc.Models
{
    public class ReaderModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }

        public ReaderModel()
        { }
        public ReaderModel(BookLibrary.Entities.Reader reader)
        {
            this.ID = reader.ID;
            this.FirstName = reader.FirstName;
            this.LastName = reader.LastName;
            this.PhoneNumber = reader.PhoneNumber;
            this.CreatedDate = reader.CreatedDate;
        }

        public void CopyValuesToEntity(BookLibrary.Entities.Reader dbBook)
        {
            dbBook.FirstName = this.FirstName;
            dbBook.LastName = this.LastName;
            dbBook.PhoneNumber = this.PhoneNumber;
            dbBook.CreatedDate = this.CreatedDate;
        }
    }
}