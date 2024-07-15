using BookHub.Domain;
using BookHub.Models;

namespace BookHub.Services
{
    public interface ICategoryService
    {

        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
    }
}
