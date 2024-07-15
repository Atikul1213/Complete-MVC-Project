using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookHub.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookHub.Models
{
    public record InventoryModel
    {
        public InventoryModel()
        {
            productList = new List<SelectListItem>();
            wareHouseList = new List<SelectListItem>();
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Quantity { get; set; }
        [Required]
        public int productId { get; set; }
        [Required]
        public int warHouseId { get; set; }

       

        [ValidateNever]
        public Product? Product { get; set; }

        [ValidateNever]
        public WareHouse WareHouse { get; set; }

        [ValidateNever]
        public Category? Category { get; set; }


        public List<SelectListItem> productList { get; set; }
        public List<SelectListItem> wareHouseList { get; set; }
       
        

    }
}
