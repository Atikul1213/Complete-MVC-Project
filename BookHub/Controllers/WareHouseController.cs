using BookHub.Data;
using BookHub.Domain;
using BookHub.Factories;
using BookHub.Models;
using BookHub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookHub.Controllers
{
    public class WareHouseController : Controller
    {
         
        private readonly IWareHouseModelFactory _wareHouseModelFactory;
        private readonly IWareHouseService _wareHouseService;


        public WareHouseController(IWareHouseModelFactory wareHouseModelFactory, IWareHouseService wareHouseService)
        {
             _wareHouseModelFactory = wareHouseModelFactory;
            _wareHouseService = wareHouseService;
        }
        public IActionResult Index()
        {
            //List<Category> obj = _db.Categories.ToList();
            List<WareHouseModel> obj = _wareHouseModelFactory.PrepareListModel();


            return View(obj);
        }



        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(WareHouseModel wareHouseModel)
        {

            if(ModelState.IsValid)
            {
                var wareHouse = new WareHouse
                {
                    Name = wareHouseModel.Name,
                   
                };
                _wareHouseService.AddWareHouse(wareHouse);

                TempData["success"] = "Created Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }



        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();


             

            var wareHouse = _wareHouseService.GetWareHouseById(id.Value);
            if(wareHouse == null)
                return NotFound();

            var wareHouseModel = new WareHouseModel()
            {
                Id = wareHouse.Id,
                Name = wareHouse.Name
            };

            return View(wareHouseModel);
        }



        [HttpPost]
        public IActionResult Edit(WareHouseModel wareHouseModel)
        {

            if (ModelState.IsValid)
            {
                var wareHouse = new WareHouse()
                {
                    Name = wareHouseModel.Name,
                    Id = wareHouseModel.Id
                    
                };
                _wareHouseService.UpdateWareHouse(wareHouse);

                TempData["success"] = "Created Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }


       



        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
                return NotFound();

            var wareHouse = _wareHouseService.GetWareHouseById(id.Value);
            if (wareHouse == null)
                return NotFound();

            var wareHouseModel = new WareHouseModel()
            {
                Id = wareHouse.Id,
                Name = wareHouse.Name
            };

            return View(wareHouseModel);

        }
        

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            if (id == null || id == 0)
                return NotFound();

            var wareHouseModel = _wareHouseService.GetWareHouseById(id.Value);
            if (wareHouseModel == null)
                return NotFound();

            var wareHouse = new WareHouse()
            {
                Name = wareHouseModel.Name,
                Id = wareHouseModel.Id

            };
            _wareHouseService.DeleteWareHouse(wareHouse.Id);

            TempData["success"] = "Delete Successfully";

            return RedirectToAction("Index");
        }




         


    }
}
