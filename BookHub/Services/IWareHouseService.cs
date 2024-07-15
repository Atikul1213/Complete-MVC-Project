using BookHub.Domain;
using BookHub.Models;

namespace BookHub.Services
{
    public interface IWareHouseService
    {

        IEnumerable<WareHouse> GetAllWareHouses();
        WareHouse GetWareHouseById(int id);
        void AddWareHouse(WareHouse wareHouse);
        void UpdateWareHouse(WareHouse wareHouse);
        void DeleteWareHouse(int id);
    }
}
