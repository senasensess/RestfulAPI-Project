using RestfulAPI_Project.Models.Entities.Concrete;

namespace RestfulAPI_Project.Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategory(int id);
        bool CategoryExists(int id);
        bool CategoryExists(string categoryName);
        bool CategoryCreate(Category category);
        bool CategoryUpdate(Category category);
        bool CategoryDelete(int id);
        bool Save();

    }
}
