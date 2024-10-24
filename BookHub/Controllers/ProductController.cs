﻿using BookHub.Data;
using BookHub.Domain;
using BookHub.Factories;
using BookHub.Models;
using BookHub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace BookHub.Controllers
{
    public class ProductController : Controller
    {
         
        private readonly IProductModelFactory _productModelFactory;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IInventoryService _inventoryService;


        public ProductController(IProductModelFactory productModelFactory, IProductService productService, IWebHostEnvironment webHostEnvironment, ICategoryService categoryService, IInventoryService inventoryService)
        {
            _productModelFactory = productModelFactory;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _categoryService = categoryService;
            _inventoryService = inventoryService;
        }
        public IActionResult Index()
        {
            
           
            var model = new ProductSearchModel
            {
                categoryList = _categoryService.GetAllCategories().Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList(),

            };


            return View(model);
        }
 
  
    

        [HttpPost]
        public IActionResult GetAll(ProductSearchModel model)
        {

            var catid = model.SelectedCategoryId;
            var prodName = model.SearchName;
            var price = model.MaxPrice;
         //   Console.WriteLine("Length: ", model.Length);
          //  Console.WriteLine("Size: ", model.Start);

            var listproduct = _productService.GetAllProducts();

            List<ProductListMode> productListModel = new List<ProductListMode>();
            List<ProductListMode> productListModelRet = new List<ProductListMode>();

            foreach (var x in listproduct)
            {
                ProductListMode obj = _productModelFactory.PrepareProductListModel(x.Id);
                 
                productListModel.Add(obj);
            }

            if (productListModel.Count > 0)
            {
                productListModelRet = productListModel.Where(x => (catid == 0 ||
                x.Items.categoryId == catid) && (prodName == null ||
                x.Items.Name.Contains(prodName)) && (price == 0 || x.Items.Price <= price)).Skip(model.Start).Take(model.Length).ToList();
            }

            // .Skip(model.pageIndex * model.pageSize).Take(model.pageSize)

            return Json(new { data = productListModelRet });
        }

 


        public IActionResult GetInventoryDetails(int productId)
        {
            // Fetch inventory details based on productId

            var prodInvList = _productModelFactory.PrepareProductListModel(productId);

            var inventories = prodInvList.InventoryItems;

            if (inventories != null && inventories.Any())
            {
                var result = inventories.Select(i => new
                {
                    quantity = i.Quantity,
                    warehouse = i.WareHouse.Name
                }).ToList();

                return Json(result);
            }

            return Json(new List<object>());
        }




        public IActionResult Create()
        {

           var CategoryList = _categoryService.GetAllCategories().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;

            return View();
        }




        [HttpPost]
        public IActionResult Create(ProductModel productModel, IFormFile? file)
        {

            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    using(var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productModel.ImageUrl = @"\images\product\" + fileName;
                }



                var product = new Product
                {
                    Name = productModel.Name,
                    Price = productModel.Price,
                    ImageUrl = productModel.ImageUrl,
                    catId = productModel.catId
                   
                };
                _productService.AddProduct(product);

                TempData["success"] = "Created Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }



        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();



            var product = _productService.GetProductById(id.Value);
            if(product == null)
                return NotFound();
            var CategoryList = _categoryService.GetAllCategories().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;

            var productModel = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                catId = product.catId
            };

            return View(productModel);
        }



        [HttpPost]


        [HttpPost]
        public IActionResult Edit(ProductModel productModel, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productModel.ImageUrl = @"\images\product\" + fileName;
                }

                _productService.DeleteProduct(productModel.Id);

                var product = new Product
                {
                    Name = productModel.Name,
                    Price = productModel.Price,
                    ImageUrl = productModel.ImageUrl,
                    catId = productModel.catId

                };

                _productService.UpdateProduct(product);

                TempData["success"] = "Created Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }





        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var product = _productService.GetProductById(id.Value);


            if (product == null)
                return NotFound();

            var CategoryList = _categoryService.GetAllCategories().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;






            //var productModel = new ProductModel()
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Price = product.Price,
            //    ImageUrl = product.ImageUrl,
            //    catId = product.catId
            //};

            var productListModel = _productModelFactory.PrepareProductListModel(id.Value);


            return View(productListModel);

        }













        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
                return NotFound();

            var product = _productService.GetProductById(id.Value);


            if (product == null)
                return NotFound();

            var CategoryList = _categoryService.GetAllCategories().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;

            var productModel = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                catId = product.catId
            };

            return View(productModel);

        }
        

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            if (id == null || id == 0)
                return NotFound();

            var productModel = _productService.GetProductById(id.Value);
            if (productModel == null)
                return NotFound();

            var product = new Product()
            {
                Name = productModel.Name,
                Id = productModel.Id,
                Price = productModel.Price,
                ImageUrl = productModel.ImageUrl,
                catId = productModel.catId

            };
            _productService.DeleteProduct(product.Id);

            TempData["success"] = "Delete Successfully";

            return RedirectToAction("Index");
        }




         


    }
}
