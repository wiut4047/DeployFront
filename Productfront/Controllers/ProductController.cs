using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Productfront.Models;
using Newtonsoft.Json;
    
namespace Productfront.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<ProductViewModel> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44385/api/product");
                //HTTP GET
                var responseTask = client.GetAsync("product");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductViewModel>>();
                    readTask.Wait();

                    products = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    products = Enumerable.Empty<ProductViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(products);
           
        }
        [HttpGet]
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(ProductViewModel product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44385/api/product");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ProductViewModel>("product", product);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(product);
        }

        public ActionResult Edit(int? id)   
        {
            ProductViewModel product = null ;
          
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44385/api/product");
                //HTTP GET
                var responseTask = client.GetAsync("product/" + id.ToString());
            
                responseTask.Wait();

                var result = responseTask.Result;

                var p  = JsonConvert.SerializeObject(result);


                if (result.IsSuccessStatusCode)
                {   
                    var readTask = result.Content.ReadAsAsync<ProductViewModel>();

                    readTask.Wait();

                    product = readTask.Result;
                }
            }

            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44385/api/product");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<ProductViewModel>("product", product);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }
    }
}
