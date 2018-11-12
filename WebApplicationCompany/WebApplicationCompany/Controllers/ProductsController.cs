using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationCompany.Models;
using System.Data.SqlClient;

namespace WebApplicationCompany.Controllers
{
    public class ProductsController : ApiController // http://localhost:65323/api/products/1
    {
        Product[] products = new Product[]
        {
        new Product { Id = 1, Name = "Чай Ахмат", Category = "Бакалея", Price = 100 },
        new Product { Id = 2, Name = "Кукла Барби", Category = "Игрушки", Price = 1000 },
        new Product { Id = 3, Name = "Дрель Интерскол", Category = "Инструменты", Price = 3000 }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //create // int gender, string name, string lname, string patronymic, DateTime empl, DateTime birth, int position,
        //string county, string region, string city, string street, int streetNumber, int apartmentNumber, string phone, int status
    }
}