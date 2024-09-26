using ASPproject.Areas.Identity.Data;
using ASPproject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace ASPproject.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PurchaseController(AuthDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
            
        }
       

        // GET: Display the order form
        public IActionResult PlaceOrder()
        {


            var model = new OrderFormViewModel
            {
                OrderNumber = GenerateOrderNumber(),
                UserName = _userManager.GetUserName(User),
                FlooringTypes = _context.Products.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.price.ToString() // Assuming 'Price' is a decimal value
                }).ToList()
            };
            return View(model);
        }

        private string GenerateOrderNumber()
        {
            Random random = new Random();
            // Generate a random uppercase letter
            char letter = (char)random.Next('A', 'Z' + 1);
            // Generate a random 4-digit number
            int digits = random.Next(1000, 10000);

            // Combine letter and digits
            return $"{letter}{digits}";
        }

        // POST: Submit the form and save the order
        [HttpPost]
        public IActionResult PlaceOrder(OrderFormViewModel model)
        {
            var FlooringTypes = _context.Products.ToList();
            if (ModelState.IsValid)
            {
               
                return View(model);
                
            }
          
            var order = new Order
            {
                OrderNumber = model.OrderNumber,
                UserName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                InstallationDate = model.InstallationDate,
                TotalPrice = model.TotalPrice,
                FlooringType=model.SelectedFlooring,
                CustomerUserName= model.UserName,
                SquareFeet= model.SquareFeet
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("OrderConfirmation",new { orderId = order.Id });
            
        }

        // Confirmation page after placing the order
        public IActionResult OrderConfirmation(int orderId)
        {
            // Fetch order details from the database
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            // Create a ViewModel to pass to the view
            var viewModel = new OrderFormViewModel
            {
                OrderNumber = order.OrderNumber,
                FullName = order.UserName,
                UserName = order.CustomerUserName,
                PhoneNumber = order.PhoneNumber,
                Address = order.Address,
                SelectedFlooring = order.FlooringType,
                InstallationDate = order.InstallationDate,
                SquareFeet = order.SquareFeet
            };

            return View(viewModel);
        }


        public IActionResult MyOrders()
        {
            var currentUserName = _userManager.GetUserName(User);

            // Fetch orders where the UserName matches the currently logged-in user
            var orders = _context.Orders
                .Where(o => o.CustomerUserName == currentUserName)
                .Select(o => new OrderFormViewModel
                {
                    OrderNumber = o.OrderNumber,
                    UserName = o.UserName,
                    PhoneNumber = o.PhoneNumber,
                    Address = o.Address,
                    InstallationDate = o.InstallationDate,
                    SelectedFlooring = o.FlooringType,
                    SquareFeet= o.SquareFeet,
                    TotalPrice= o.TotalPrice
                })
                .ToList();

            return View(orders);
        }
    }
}
    