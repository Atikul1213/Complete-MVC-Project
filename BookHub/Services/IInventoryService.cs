using BookHub.Domain;
using BookHub.Models;

namespace BookHub.Services
{
    public interface IInventoryService
    {

        IEnumerable<Inventory> GetAllInventors();
        Inventory GetInventoryById(int id);
        void AddInventory(Inventory inventory);
        void UpdateInventory(Inventory inventory);
        void DeleteInventory(int id);

        Inventory GetInventory(int pid, int wid);
    }
}
