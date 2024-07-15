using BookHub.Data;
using BookHub.Domain;
using BookHub.Factories;
using BookHub.Models;
using BookHub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BookHub.Controllers
{
    public class InventoryController : Controller
    {
         
        private readonly IInventoryService _inventoryService;
        private readonly IInventoryModelFactory _inventoryModelFactory;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWareHouseService _warehouseService;
        


        public InventoryController(IInventoryModelFactory inventoryModelFactory, IInventoryService inventoryService, ICategoryService categoryService,IProductService productService, IWareHouseService wareHouseService)
        {
            _inventoryService = inventoryService;
            _inventoryModelFactory = inventoryModelFactory; 
            _productService = productService;
            _categoryService = categoryService;
            _warehouseService = wareHouseService;
        }
        public IActionResult Index()
        {
             
            var obj = _inventoryModelFactory.PrepareInventoryListModel();


            return View(obj);
        }






        public IActionResult Create()
        {

            var model = new InventoryModel
            {
                productList = _productService.GetAllProducts().Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList(),

                wareHouseList = _warehouseService.GetAllWareHouses().Select(w => new SelectListItem
                {
                    Text = w.Name,
                    Value = w.Id.ToString()
                }).ToList()
            };




            return View(model);
        }



        public IActionResult GetQuantity(int productId, int warHouseId)
        {

            var model =  _inventoryService.GetInventory(productId, warHouseId);


            // int quantity = _inventoryService.GetQuantity(productId, warHouseId);
            if (model == null)
                return Json(model);

            return Json(model.Quantity);
        }



       
        [HttpPost]
        public IActionResult Create(InventoryModel inventoryModel)
        {

            if(ModelState.IsValid)
            {
                int pid = inventoryModel.productId;
                int wid = inventoryModel.warHouseId;

                var currInventory = _inventoryService.GetInventory(pid, wid);

                
                if (currInventory!=null && currInventory.Id>0)
                {
                    currInventory.Quantity= inventoryModel.Quantity;

                    _inventoryService.UpdateInventory(currInventory);

 
                }
                else {


                    var Inventory = new Inventory
                    {
                        Quantity = inventoryModel.Quantity,
                        productId = inventoryModel.productId,
                        warHouseId = inventoryModel.warHouseId
                    };

                    _inventoryService.AddInventory(Inventory);
                }

                TempData["success"] = "Created Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }



        



        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();


            var inventory = _inventoryService.GetInventoryById(id.Value);

             
            if(inventory == null)
                return NotFound();




            var inventoryModel = new InventoryModel
            { 
                Id = inventory.Id,
                Quantity = inventory.Quantity,
                productId = inventory.productId,
                warHouseId = inventory.warHouseId,

                productList = _productService.GetAllProducts().Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList(),

                wareHouseList = _warehouseService.GetAllWareHouses().Select(w => new SelectListItem
                {
                    Text = w.Name,
                    Value = w.Id.ToString()
                }).ToList()

            };




            return View(inventoryModel);
        }





        [HttpPost]
        public IActionResult Edit(InventoryModel inventoryModel)
        {

            if (ModelState.IsValid)
            {
               
                _inventoryService.DeleteInventory(inventoryModel.Id);


                var Inventory = new Inventory
                {
                    Quantity = inventoryModel.Quantity,
                    productId = inventoryModel.productId,
                    warHouseId = inventoryModel.warHouseId
                };

                _inventoryService.UpdateInventory(Inventory);
                

                TempData["success"] = "Created Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }




        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
                return NotFound();



            var inventory = _inventoryService.GetInventoryById(id.Value);


            if (inventory == null)
                return NotFound();


            var inventoryModel = new InventoryModel
            {
                Id = inventory.Id,
                Quantity = inventory.Quantity,
                productId = inventory.productId,
                warHouseId = inventory.warHouseId,

                productList = _productService.GetAllProducts().Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList(),

                wareHouseList = _warehouseService.GetAllWareHouses().Select(w => new SelectListItem
                {
                    Text = w.Name,
                    Value = w.Id.ToString()
                }).ToList()

            };

            return View(inventoryModel);

        }
        
        

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            if (id == null || id == 0)
                return NotFound();

            var inventoryModel = _inventoryService.GetInventoryById(id.Value);

            
            if (inventoryModel == null)
                return NotFound();

            var inventory = new Inventory
            {
                Quantity = inventoryModel.Quantity,
                productId = inventoryModel.productId,
                warHouseId = inventoryModel.warHouseId,
                Id = inventoryModel.Id
            };

            _inventoryService.DeleteInventory(inventory.Id);
            
            TempData["success"] = "Delete Successfully";

            return RedirectToAction("Index");
        }



        #region API Call

        [HttpGet]
        public IActionResult GetAll()
        {
            var obj = _inventoryModelFactory.PrepareInventoryListModel();
            return Json(new { data = obj.Items });
        }


        
        #endregion




    }
}
