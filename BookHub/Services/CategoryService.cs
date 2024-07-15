using BookHub.Domain;
using BookHub.Factories;
using BookHub.Models;
using BookHub.Repository;

namespace BookHub.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository ;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.Getall();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.Get(c => c.Id == id);
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.Add(category);
            
        }

        public void UpdateCategory(Category category)
        {
  
            _categoryRepository.Update(category);
        }

        public void DeleteCategory(int id)
        {
            var category = _categoryRepository.Get(c => c.Id == id);
            _categoryRepository.Remove(category);
        }
    }
}
