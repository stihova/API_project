using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookRepository : BaseRepository<Book>
    {
        public override void Save(Book book)
        {
            if (book.BookID == 0)
            {
                base.Create(book);
            }
            else
            {
                base.Update(book, item => item.BookID == book.BookID);
            }
        }

        public List<Book> GetBookByName(string name)
        {
            return base.GetAll().Where(book => string.IsNullOrEmpty(name) || book.BookName.Contains(name)).ToList();
        }
    }
}
