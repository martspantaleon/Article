using Article.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ArticleMVC.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
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

        public ActionResult AddArticle()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddArticle(ArticleViewModel article)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50988/api/article/AddArticle");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ArticleViewModel>("AddArticle", article);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(article);
        }

        public ActionResult ViewArticle(int id)
        {
            IEnumerable<ArticleViewModel> article = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50988/api/article/GetArticle");
                //HTTP GET
                var responseTask = client.GetAsync("GetArticle/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ArticleViewModel>>();
                    readTask.Wait();

                    article = readTask.Result;
                }
            }

            return View("ViewArticle", article);
        }

        public ActionResult EditArticle(int id)
        {
            IEnumerable<ArticleViewModel> article = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50988/api/article/GetArticle");
                //HTTP GET
                var responseTask = client.GetAsync("GetArticle/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<ArticleViewModel>>();
                    readTask.Wait();

                    article = readTask.Result;
                }
            }

            return View(article);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditArticle(ArticleViewModel article)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50988/api/article/EditArticle");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<ArticleViewModel>("EditArticle", article);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(article);
        }

    }
}