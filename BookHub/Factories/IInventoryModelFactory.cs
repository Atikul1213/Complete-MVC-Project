using BookHub.Domain;
using BookHub.Models;

namespace BookHub.Factories
{
    public interface IInventoryModelFactory
    {

        InventoryModel PrepareInventoryModel(Inventory inventory);
        InventoryListMode PrepareInventoryListModel();

    }
}
