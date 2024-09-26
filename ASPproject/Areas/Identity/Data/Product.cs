using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ASPproject.Areas.Identity.Data
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Brand { get; set; }
        [Precision(16,2)]
        public decimal price { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string ImageFileName { get; set; }
    }
}
