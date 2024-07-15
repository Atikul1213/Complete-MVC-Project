using System.Collections;


namespace BookHub.Models
{
    public class InventoryListMode
    {
        public InventoryListMode()
        {
            Items = new List<InventoryModel>();
        }
        public IList<InventoryModel> Items { get; set; }

        public string Title { get; set; }
    }
}
