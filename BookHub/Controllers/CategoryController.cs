using BookHub.Data;
using BookHub.Domain;
using BookHub.Factories;
using BookHub.Models;
using BookHub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookHub.Controllers
{
    public class CategoryController : Controller
    {
         
        private readonly ICategoryModelFactory _categoryModelFactory;
        private readonly ICategoryService _categoryService;


        public CategoryController(ICategoryModelFactory categoryModelFactory, ICategoryService categoryService)
        {
            _categoryModelFactory = categoryModelFactory;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            //List<Category> obj = _db.Categories.ToList();
            List<CategoryModel> obj = _categoryModelFactory.PrepareListModel();


            return View(obj);
        }



        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryModel categoryModel)
        {

            if(ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = categoryModel.Name
                   
                };
                _categoryService.AddCategory(category);
                 

                TempData["success"] = "Created Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }



        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();


             

            var category = _categoryService.GetCategoryById(id.Value);
            if(category == null)
                return NotFound();

            var categoryModel = new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(categoryModel);
        }



        [HttpPost]
        public IActionResult Edit(CategoryModel categoryModel)
        {

            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    Name = categoryModel.Name,
                    Id = categoryModel.Id
                    
                };
                _categoryService.UpdateCategory(category);

                TempData["success"] = "Created Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }


       



        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
                return NotFound();

            var category = _categoryService.GetCategoryById(id.Value);
            if (category == null)
                return NotFound();

            var categoryModel = new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(categoryModel);

        }
        

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            if (id == null || id == 0)
                return NotFound();

            var categoryModel = _categoryService.GetCategoryById(id.Value);
            if (categoryModel == null)
                return NotFound();

            var category = new Category()
            {
                Name = categoryModel.Name,
                Id = categoryModel.Id

            };
            _categoryService.DeleteCategory(category.Id);

            TempData["success"] = "Delete Successfully";

            return RedirectToAction("Index");
        }




         


    }
}
