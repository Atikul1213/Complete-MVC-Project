using BookHub.Domain;
using BookHub.Models;

namespace BookHub.Factories
{
    public interface ICategoryModelFactory
    {

        
         CategoryModel PrepareCategoryModel( Category category);

         List<CategoryModel> PrepareListModel();
    }
}
