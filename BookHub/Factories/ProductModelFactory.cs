using BookHub.Domain;
using BookHub.Models;
using BookHub.Repository;
using BookHub.Services;

namespace BookHub.Factories
{
    public class ProductModelFactory : IProductModelFactory
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IInventoryService _inventoryService;
        private readonly IWareHouseService _wareHouseService;

        public ProductModelFactory(IProductService productService, ICategoryService categoryService, IInventoryService inventoryService, IWareHouseService wareHouseService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _inventoryService = inventoryService;
            _wareHouseService = wareHouseService;
        }




        public ProductModel PrepareProductModel(Product product)
        {
            if (product == null)
                throw new ArgumentNullException();

            ProductModel model = new ProductModel();
            model.Name = product.Name;
            model.Id = product.Id;
            model.Price = product.Price;
            model.ImageUrl = product.ImageUrl;
            model.catId = product.catId;
            model.Category = _categoryService.GetCategoryById(product.catId);
            model.productId = product.Id;
            model.categoryId = product.catId;
            

            return model;
        }

        public List<ProductModel> PrepareListModel()
        {

            return _productService.GetAllProducts()
                  .Select(cat => new ProductModel { Name = cat.Name , Id = cat.Id, Price = cat.Price, catId = cat.catId, ImageUrl= cat.ImageUrl,categoryId=cat.catId,productId=cat.Id,  Category= _categoryService.GetCategoryById(cat.catId)})
                  .ToList();

        }




        public ProductListMode PrepareProductListModel(int id)
        {
            var model = new ProductListMode();

            var product = _productService.GetProductById(id);

            var productModel = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                catId = product.catId,
                productId = product.Id,
                categoryId = product.catId,
                Category = _categoryService.GetCategoryById(product.catId)
            };


            model.Items = productModel;


            model.InventoryItems = _inventoryService.GetAllInventors().Where(x=>x.productId==id)
                        .Select(x => new InventoryModel
                        {
                            Quantity = x.Quantity,
                            Id = x.Id,
                            productId = x.productId,
                            warHouseId = x.warHouseId,
                            Product = _productService.GetProductById(x.productId),
                            WareHouse = _wareHouseService.GetWareHouseById(x.warHouseId)
                            

                        }).ToList();

            model.InventoryItems = model.InventoryItems.OrderByDescending(model=> model.Quantity).ThenBy(model=>model.WareHouse.Name).ToList();


            model.Title = "Product";

            return model;

        }






    }
}
