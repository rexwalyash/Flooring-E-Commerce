using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ASPproject.Areas.Identity.Data
{
    public class OrderFormViewModel
    {
        public string OrderNumber { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public List<SelectListItem> FlooringTypes { get; set; }
        [Required]
        public DateTime InstallationDate { get; set; }=DateTime.Now;
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Square Feet must be greater than 0.")]
        public int SquareFeet { get; set; } 
        public string SelectedFlooring {  get; set; }

        public decimal TotalPrice { get; set; } 
        public string UserName { get; set; }
    }
}


