using BookHub.Domain;
using BookHub.Models;

namespace BookHub.Factories
{
    public interface IWareHouseModelFactory
    {


        WareHouseModel PrepareWareHouseModel(WareHouse wareHouse);

         List<WareHouseModel> PrepareListModel();
    }
}
