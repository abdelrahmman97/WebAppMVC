using Microsoft.EntityFrameworkCore;
using Models;

namespace AppRepository
{
    public class CategoryRepo : MainRepo<Category>
    {
        public CategoryRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext) { }

        //public IQueryable<Category> List()=> GetList();

        public Category GetCategoryByID(int id)
        {
            return GetList().Where(i => i.ID == id).FirstOrDefault();
        }

        public void Edit(Category category)
        {
            Category old = GetCategoryByID(category.ID);
            old.Name = category.Name;
            old.Description = category.Description;
            Update(old);
        }

        public void Delete(int id)
        {
            Category category= GetCategoryByID(id);
            Delete(category);
        }

    }
}
