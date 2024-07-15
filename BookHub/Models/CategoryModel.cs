using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookHub.Models
{
    public  record CategoryModel
    {
        
        public  int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }
    }
}
