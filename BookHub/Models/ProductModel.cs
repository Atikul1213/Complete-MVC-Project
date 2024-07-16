using BookHub.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHub.Models
{
    public record ProductModel
    {
        public ProductModel()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("ProductName")]
        public string Name { get; set; }

        [DisplayName("Price")]
        public int Price { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

        public int productId { get; set; }
        public int categoryId { get; set; }
 
        [Required]
        [DisplayName("Category")]
         public int catId { get; set; }
        [ValidateNever]

        public  Category? Category { get; set; }


    }
}
