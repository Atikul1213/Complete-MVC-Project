using System.Collections;


namespace BookHub.Models
{
    public class ProductListMode
    {
        public ProductListMode()
        {
            Items = new ProductModel();
            InventoryItems = new List<InventoryModel>();
        }
        public ProductModel Items { get; set; }
        public IList<InventoryModel> InventoryItems { get; set; }
        public string Title { get; set; }
    }
}
