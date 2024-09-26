using ASPproject.Areas.Identity.Data;
using ASPproject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPproject.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly AuthDbContext context;
        private readonly IWebHostEnvironment environment;

        public ProductsController(AuthDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            if (productDto.ImageFileName == null)
            {
                ModelState.AddModelError("ImageFileName", "Image file is required");
            }

            if (!ModelState.IsValid)
            {
                return View(productDto);
            }


            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(productDto.ImageFileName!.FileName);

            string imageFullPath = environment.WebRootPath + "/Photos/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productDto.ImageFileName.CopyTo(stream);
            }

            Product product = new Product()
            {
                Name = productDto.Name,
                Brand = productDto.Brand,
                price = productDto.price,
                Description = productDto.Description,
                ImageFileName = newFileName,

            };

            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("Index", "Products");
        }

        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }

            var productDto = new ProductDto()
            {
                Name = product.Name,
                Brand = product.Brand,
                price = product.price,
                Description = product.Description,
            };

            ViewData["ProductId"] = product.Id;
            ViewData["ImageFileName"] = product.ImageFileName;


            return View(productDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductDto productDto)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }

            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = product.Id;
                ViewData["ImageFileName"] = product.ImageFileName;
                return View(productDto);
            }

            string newFileName=product.ImageFileName;
            if (productDto.ImageFileName != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(productDto.ImageFileName.FileName);

                string imageFullPath = environment.WebRootPath + "/Photos/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    productDto.ImageFileName.CopyTo(stream);
                }

                string oldImageFullPath = environment.WebRootPath + "/Photos/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);



            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.price = productDto.price;
            product.Brand=productDto.Brand;
            product.ImageFileName = newFileName;

            context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }


        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }

            string imageFullPath=environment.WebRootPath +"/Photos/"+product.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Products.Remove(product);
            context.SaveChanges(true);
            return RedirectToAction("Index", "Products");

        }

    }

}
