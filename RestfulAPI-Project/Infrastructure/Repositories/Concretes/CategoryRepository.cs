using RestfulAPI_Project.Infrastructure.Context;
using RestfulAPI_Project.Infrastructure.Repositories.Interfaces;
using RestfulAPI_Project.Models.Entities.Concrete;

namespace RestfulAPI_Project.Infrastructure.Repositories.Concretes
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CategoryCreate(Category category)
        {
            _context.Categories.Add(category);
            return Save();
        }

        public bool CategoryUpdate(Category category)
        {
            category.UpdatedDate = DateTime.Now;
            category.Status = Models.Entities.Abstract.Status.Modified;
            _context.Categories.Update(category);
            return Save();
        }

        public bool CategoryDelete(int id)
        {
            var category = _context.Categories.Find(id);
            category.DeletedDate = DateTime.Now;
            category.Status = Models.Entities.Abstract.Status.Passive;
            _context.Categories.Update(category);
            return Save();
        }

        public bool CategoryExists(int id) => _context.Categories.Any(x => x.Id == id);

        public bool CategoryExists(string categoryName) => _context.Categories.Any(x => x.Name == categoryName);

        public List<Category> GetCategories() => _context.Categories.Where(x => x.Status != Models.Entities.Abstract.Status.Passive).ToList();

        public Category GetCategory(int id) => _context.Categories.Where(x => x.Status != Models.Entities.Abstract.Status.Passive).FirstOrDefault(x => x.Id == id);

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
