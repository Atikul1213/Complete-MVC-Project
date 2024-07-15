using BookHub.Domain;
using BookHub.Models;
using BookHub.Repository;
using BookHub.Services;

namespace BookHub.Factories
{
    public class WareHouseModelFactory : IWareHouseModelFactory
    {
        private readonly IWareHouseService _wareHouseService;

        public WareHouseModelFactory(IWareHouseService wareHouseService)
        {
            _wareHouseService = wareHouseService; 
        }




        public WareHouseModel PrepareWareHouseModel(WareHouse wareHouse)
        {
            if (wareHouse == null)
                throw new ArgumentNullException();

            WareHouseModel model = new WareHouseModel();
            model.Name = wareHouse.Name;
            model.Id = wareHouse.Id;
            return model;
        }

        public List<WareHouseModel> PrepareListModel()
        {

            return _wareHouseService.GetAllWareHouses()
                  .Select(cat => new WareHouseModel { Name = cat.Name , Id = cat.Id})
                  .ToList();

        }
    }
}
