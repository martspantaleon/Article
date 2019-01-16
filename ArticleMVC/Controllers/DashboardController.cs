using Article.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ArticleMVC.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            IEnumerable<ArticleViewModel> model = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50988/api/article/Article");
                //HTTP GET
                var responseTask = client.GetAsync("article");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<ArticleViewModel>>();
                    readTask.Wait();

                    model = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    model = Enumerable.Empty<ArticleViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(model);
        }

        public ActionResult Test()
        {
            IEnumerable<ArticleViewModel> model = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50988/api/");
                //HTTP GET
                var responseTask = client.GetAsync("article");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<ArticleViewModel>>();
                    readTask.Wait();

                    model = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    model = Enumerable.Empty<ArticleViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(model);
        }
    }
}