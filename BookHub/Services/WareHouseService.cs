using BookHub.Domain;
using BookHub.Factories;
using BookHub.Models;
using BookHub.Repository;

namespace BookHub.Services
{
    public class WareHouseService : IWareHouseService
    {
        private readonly IRepository<WareHouse> _wareHouseRepository;

        public WareHouseService(IRepository<WareHouse> wareHouseRepository)
        {
            _wareHouseRepository = wareHouseRepository;
        }

        public IEnumerable<WareHouse> GetAllWareHouses()
        {
            return _wareHouseRepository.Getall();
        }

        public WareHouse GetWareHouseById(int id)
        {
            return _wareHouseRepository.Get(c => c.Id == id);
        }

        public void AddWareHouse(WareHouse wareHouse)
        {
            _wareHouseRepository.Add(wareHouse);
        }

        public void UpdateWareHouse(WareHouse wareHouse)
        {

            _wareHouseRepository.Update(wareHouse);
        }

        public void DeleteWareHouse(int id)
        {
            var wareHouse = _wareHouseRepository.Get(c => c.Id == id);
            _wareHouseRepository.Remove(wareHouse);
        }
    }
}
