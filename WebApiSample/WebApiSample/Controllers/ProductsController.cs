using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "The Incredible Hulk", Category = "Movies", Price = 200},
            new Product { Id = 2, Name = "Pepsi 200 ML", Category = "Beverages", Price = 15},
            new Product { Id = 3, Name = "MI Redmi 2",  Category = "Mobiles", Price = 6999 },
            new Product { Id = 4, Name = "Redmi MI 4i",  Category = "Mobiles", Price = 14999 },
            new Product { Id = 5, Name = "Sony Vaio Laptop",  Category = "Laptops", Price = 120000 },
            new Product { Id = 6, Name = "Lenevo Thinkpad T440p",  Category = "Laptops", Price = 155000 }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
    }
}
