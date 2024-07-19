using BookHub.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookHub.Models
{
    public class ProductSearchModel
    {
        public ProductSearchModel()
        {
            categoryList = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public List<SelectListItem> categoryList { get; set; }
        public string SearchName { get; set; }

        public int MaxPrice { get; set; }
        public int SelectedCategoryId { get; set; }

        public int Start { get; set; }
        public int Length { get; set; }


    }
}
