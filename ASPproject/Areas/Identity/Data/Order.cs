using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASPproject.Areas.Identity.Data
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        public string FlooringType {  get; set; }

      
        public DateTime InstallationDate { get; set; }

        [Required]
        public string CustomerUserName { get; set; }

        [Required]
        public int SquareFeet {  get; set; }


    }
}
