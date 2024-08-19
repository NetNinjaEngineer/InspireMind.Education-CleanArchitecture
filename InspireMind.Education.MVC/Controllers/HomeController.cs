using InspireMind.Education.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace InspireMind.Education.MVC.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private List<Product> products;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 999.99m },
            new Product { Id = 2, Name = "Smartphone", Category = "Electronics", Price = 699.99m },
            new Product { Id = 3, Name = "Headphones", Category = "Electronics", Price = 199.99m },
            new Product { Id = 4, Name = "Smartwatch", Category = "Electronics", Price = 249.99m },
            new Product { Id = 5, Name = "Tablet", Category = "Electronics", Price = 329.99m },
            new Product { Id = 6, Name = "Camera", Category = "Electronics", Price = 449.99m },
            new Product { Id = 7, Name = "Bluetooth Speaker", Category = "Electronics", Price = 129.99m },
            new Product { Id = 8, Name = "Gaming Console", Category = "Electronics", Price = 499.99m },
            new Product { Id = 9, Name = "TV", Category = "Electronics", Price = 1199.99m },
            new Product { Id = 10, Name = "Monitor", Category = "Electronics", Price = 299.99m },
            new Product { Id = 11, Name = "Desk Chair", Category = "Furniture", Price = 149.99m },
            new Product { Id = 12, Name = "Dining Table", Category = "Furniture", Price = 599.99m },
            new Product { Id = 13, Name = "Sofa", Category = "Furniture", Price = 799.99m },
            new Product { Id = 14, Name = "Bookshelf", Category = "Furniture", Price = 199.99m },
            new Product { Id = 15, Name = "Bed Frame", Category = "Furniture", Price = 399.99m },
            new Product { Id = 16, Name = "Mattress", Category = "Furniture", Price = 499.99m },
            new Product { Id = 17, Name = "Coffee Maker", Category = "Home Appliances", Price = 89.99m },
            new Product { Id = 18, Name = "Blender", Category = "Home Appliances", Price = 59.99m },
            new Product { Id = 19, Name = "Microwave Oven", Category = "Home Appliances", Price = 129.99m },
            new Product { Id = 20, Name = "Air Purifier", Category = "Home Appliances", Price = 199.99m }
        };

    }

    public IActionResult Index(int pageNumber = 1, int pageSize = 5, string? searchTerm = null, string? orderBy = null, int? id = null)
    {
        ViewBag.NameSortParam = string.IsNullOrEmpty(orderBy) ? "NameAsc" : "";


        var totalCount = products.Count;

        if (!String.IsNullOrEmpty(searchTerm))
        {
            products = products.Where(p => p.Name.Contains(searchTerm) || p.Category.Contains(searchTerm)).ToList();
        }

        var pagedResults = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        PagedList<Product> pagedList = default;

        if (id.HasValue)
        {
            var selectedProduct = products.FirstOrDefault(x => x.Id == id.Value);

            pagedList = new PagedList<Product>(pageNumber, pageSize, totalCount, pagedResults, selectedProduct);
            return View(pagedList);
        }

        if (!string.IsNullOrEmpty(orderBy))
        {
            switch (orderBy)
            {
                case "NameAsc":
                    var orderedPagedResult = pagedResults.OrderBy(x => x.Name).ToList();
                    pagedList = new PagedList<Product>(pageNumber, pageSize, totalCount, orderedPagedResult, null);
                    return View(pagedList);
                default:
                    pagedList = new PagedList<Product>(pageNumber, pageSize, totalCount, pagedResults.OrderByDescending(x => x.Name).ToList(), null);
                    return View(pagedList);
            }
        }


        pagedList = new PagedList<Product>(pageNumber, pageSize, totalCount, pagedResults, null);

        return View(pagedList);

    }

    public IActionResult Details(int id)
    {
        var product = products.Where(x => x.Id == id).FirstOrDefault();

        if (product == null)
        {
            return NotFound();
        }

        return PartialView("_ProductDetailsPartial", product);

        //return Json(new
        //{
        //    product.Id,
        //    product.Name,
        //    product.Description,
        //    product.Price,
        //    product.ImageUrl,
        //    product.Category
        //});

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
