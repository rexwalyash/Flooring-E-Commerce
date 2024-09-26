using ASPproject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ASPproject.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using ASPproject.Areas.Identity.Data;
using System.Threading.Tasks;


namespace ASPproject.Controllers
{
    public class AdminOrdersController : Controller
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly AuthDbContext _context;

        public AdminOrdersController(AuthDbContext context)
        {
            
            _context = context;
        }

        public IActionResult Reports()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetReportData(string category)
        {
            var orders = _context.Orders.AsQueryable();
            List<object> reportData = new List<object>();

            switch (category)
            {
                case "CustomerName":
                    reportData = orders.GroupBy(o => o.UserName)
                                       .Select(g => new { Label = g.Key, Count = g.Count() })
                                       .ToList<object>();
                    break;

               

                case "FlooringType":
                    reportData = orders.GroupBy(o => o.FlooringType)
                                       .Select(g => new { Label = g.Key, Count = g.Count() })
                                       .ToList<object>();
                    break;

                case "Price":
                    reportData = orders.GroupBy(o =>
                                               o.TotalPrice <= 50 ? "0-50" :
                                               o.TotalPrice <= 100 ? "50-100" :
                                               o.TotalPrice <= 150 ? "100-150" :                                             
                                               o.TotalPrice <= 200 ? "150-200" :
                                               o.TotalPrice <= 300 ? "200-300" :
                                               o.TotalPrice <= 500 ? "300-500" :
                                               o.TotalPrice <= 1000 ? "500-1000" :
                                               "1000+")
                                       .Select(g => new { Label = g.Key, Count = g.Count() })
                                       .ToList<object>();
                    break;
            }

            return Json(reportData);
        }

        public IActionResult GetSalesData()
        {
            var salesData = _context.Orders
                .GroupBy(o => o.FlooringType)
                .Select(g => new
                {
                    flooringType = g.Key,
                    totalSales = g.Sum(o => o.TotalPrice)
                }).ToList();

            return Json(salesData);
        }

        public IActionResult GetTableReportData()
        {
            var salesData = _context.Orders
                .GroupBy(o => o.FlooringType)
                .Select(g => new
                {
                    flooringType = g.Key,
                    totalSquareFeet = g.Sum(o => o.SquareFeet)
                }).ToList();

            return Json(salesData);
        }


    }
}

