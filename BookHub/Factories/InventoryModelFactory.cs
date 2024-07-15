using BookHub.Domain;
using BookHub.Models;
using BookHub.Repository;
using BookHub.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookHub.Factories
{
    public class InventoryModelFactory : IInventoryModelFactory
    {
        private readonly IInventoryService _inventoryService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IWareHouseService _warehouseService;
        public InventoryModelFactory(IInventoryService inventoryService, ICategoryService categoryService, IProductService productService, IWareHouseService warehouseService)
        {
            _inventoryService = inventoryService;
            _categoryService = categoryService;
            _productService = productService;
            _warehouseService = warehouseService;
        }




        public InventoryModel PrepareInventoryModel(Inventory inventory)
        {
            if (inventory == null)
                throw new ArgumentNullException();

            InventoryModel model = new InventoryModel();
            
            model.Quantity = inventory.Quantity;
            model.productId = inventory.productId;
            model.Id = inventory.Id;
            model.warHouseId = inventory.warHouseId;

            model.Product = _productService.GetProductById(inventory.productId);
            model.WareHouse = _warehouseService.GetWareHouseById(inventory.warHouseId);

            //  model.Category = _categoryService.GetCategoryById(model.Product.catId);

            


            return model;
        }

        public  InventoryListMode PrepareInventoryListModel()
        {
            var model = new InventoryListMode();
            model.Items=  _inventoryService.GetAllInventors()
                       .Select(x=> new InventoryModel
                       {
                           Quantity = x.Quantity,
                           Id = x.Id,
                           productId = x.productId,
                           warHouseId = x.warHouseId,
                           Product = _productService.GetProductById(x.productId),
                           WareHouse = _warehouseService.GetWareHouseById(x.warHouseId)

                       }).ToList();
            model.Title = "Inventory";

            return model;

        }



    }
}
