using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public override void Save(Category category)
        {
            if (category.CategoryID == 0)
            {
                base.Create(category);
            }
            else
            {
                base.Update(category, item => item.CategoryID == category.CategoryID);
            }
        }

        public List<Category> GetCategoryByName(string name)
        {
            return base.GetAll().Where(book => string.IsNullOrEmpty(name) || book.CateoryName.Contains(name)).ToList();
        }

    }
}
