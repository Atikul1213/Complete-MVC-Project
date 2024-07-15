using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookHub.Models
{
    public record WareHouseModel
    {
       
        public int Id { get; set; }
        [Required]
        [DisplayName("WareHouse Name")]
        public string Name { get; set; }
    }
}
