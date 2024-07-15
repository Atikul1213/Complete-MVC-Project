using BookHub.Domain;
using BookHub.Models;

namespace BookHub.Factories
{
    public interface IProductModelFactory
    {

        
         ProductModel PrepareProductModel(Product product);

         List<ProductModel> PrepareListModel();


        ProductListMode PrepareProductListModel(int id);
    }
}
