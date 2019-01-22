using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AuthorRepository : BaseRepository<Author>
    {
        public override void Save(Author author)
        {
            if (author.AuthorID == 0)
            {
                base.Create(author);
            }
            else
            {
                base.Update(author, item => item.AuthorID == author.AuthorID);
            }
        }
        public List<Author> GetAuthorByName(string name)
        {
            return base.GetAll().Where(book => string.IsNullOrEmpty(name) || book.AuthorName.Contains(name)).ToList();
        }
    }
}
