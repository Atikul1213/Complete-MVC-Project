using BookHub.Domain;
using BookHub.Factories;
using BookHub.Models;
using BookHub.Repository;

namespace BookHub.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IRepository<Inventory> _inventoryRepository;

        public InventoryService(IRepository<Inventory> inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public IEnumerable<Inventory> GetAllInventors()
        {
            return _inventoryRepository.Getall();
        }

        public Inventory GetInventoryById(int id)
        {
            return _inventoryRepository.Get(c => c.Id == id);
        }

        public void AddInventory(Inventory inventory)
        {
            _inventoryRepository.Add(inventory);
            
        }

        public void UpdateInventory(Inventory inventory)
        {

            _inventoryRepository.Update(inventory);
        }

        public void DeleteInventory(int id)
        {
            var inventory = _inventoryRepository.Get(c => c.Id == id);
            _inventoryRepository.Remove(inventory);
        }

        public Inventory GetInventory(int pid, int wid)
        {
 
           var inventory = (_inventoryRepository.Getall().Where(x=>x.productId==pid && x.warHouseId==wid)).FirstOrDefault();
            if (inventory==null)
                return null;
            return inventory;


        }
    }
}
