
using System.ComponentModel.DataAnnotations;

namespace ASPproject.Areas.Identity.Data
{
    public class ProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        [Required]
        public decimal price { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        
        public IFormFile? ImageFileName { get; set; }
    }
}
