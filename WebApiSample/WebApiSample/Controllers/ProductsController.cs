using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    public class ProductsController : ApiController
    {
        public IEnumerable<Product> GetAllProducts()
        {
            return readProductsFromSqlDb();
        }

        public IHttpActionResult GetProduct(int id)
        {
            var con = openConnection();
            var cmd = con.CreateCommand();
            cmd.CommandText = string.Format("select * from products where id = {0}", id);
            var rdr = cmd.ExecuteReader();
            Product product = null;
            if (rdr.Read())
                product = createProduct(rdr);

            con.Close();

            if (product == null)
                return NotFound();
            
            return Ok(product);
        }

        private Product[] readProductsFromSqlDb()
        {
            var con = openConnection();
            var cmd = con.CreateCommand();
            cmd.CommandText = "select * from products";
            var rdr = cmd.ExecuteReader();
            List<Product> products = new List<Product>();
            while (rdr.Read())
                products.Add(createProduct(rdr));

            con.Close();
            return products.ToArray();
        }

        private IDbConnection openConnection()
        {
            var con = new SqlConnection(@"Server=68ae607d-8602-4a6a-98ca-a4ca00b9cc75.sqlserver.sequelizer.com;Database=db68ae607d86024a6a98caa4ca00b9cc75;User ID=zodlhhlmghrqjfop;Password=safPsqQT2yMa22HZDXViJSfLZScBCN2aVX6cqfiMZwH2MALvRXhGyzbH4BWHnopm;");
            con.Open();
            return con;
        }

        private Product createProduct(IDataReader rdr)
        {
            return new Product
            {
                Id = Convert.ToInt32(rdr["id"]),
                Name = rdr["name"].ToString(),
                Category = rdr["category"].ToString(),
                Price = Convert.ToDecimal(rdr["price"])
            };

        }
    }
}
