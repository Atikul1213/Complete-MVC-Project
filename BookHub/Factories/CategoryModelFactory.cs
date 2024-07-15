using BookHub.Domain;
using BookHub.Models;
using BookHub.Repository;
using BookHub.Services;

namespace BookHub.Factories
{
    public class CategoryModelFactory : ICategoryModelFactory
    {
        private readonly ICategoryService _categoryService;

        public CategoryModelFactory(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }




        public CategoryModel PrepareCategoryModel(Category category)
        {
            if (category == null)
                throw new ArgumentNullException();

            CategoryModel model = new CategoryModel();
            model.Name = category.Name;
            model.Id = category.Id;
            return model;
        }

        public List<CategoryModel> PrepareListModel()
        {

            return _categoryService.GetAllCategories()
                  .Select(cat => new CategoryModel { Name = cat.Name , Id = cat.Id})
                  .ToList();

        }
    }
}
