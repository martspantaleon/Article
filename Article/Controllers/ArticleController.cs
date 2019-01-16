using Article.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Article.Controllers
{
    [RoutePrefix("api/article")]
    public class ArticleController : ApiController
    {
        [Route("Article")]
        [HttpGet]
        public IHttpActionResult GetArticles()
        {
            IEnumerable<ArticleViewModel> articles = null;

            using (var db = new ArticleEntities())
            {
                articles = db.Articles.Include("Article")
                            .Select(x => new ArticleViewModel()
                            {
                                ArticleID = x.ArticleID,
                                Title = x.Title,
                                ByLine = x.ByLine,
                                isCarousel = x.isCarousel,
                                isImportant = x.isImportant,
                                PageContent = x.PageContent,
                                Author = x.Author,
                                PublishStartDate = x.PublishStartDate,
                                PublishStartTime = x.PublishStartTime,
                                PublishEndDate = x.PublishEndDate,
                                PublishEndTime = x.PublishEndTime,
                                Category = x.Category,
                                ImageHeader = x.imageHeader,
                                isDeleted = x.isDeleted
                            }).ToList<ArticleViewModel>();
            }

            //if (articles.Count == 0)
            //{
            //    return NotFound();
            //}

            return Ok(articles);
        }

        //public IHttpActionResult GetArticleByID(int id)
        //{
        //    ArticleViewModel article = null;

        //    using (var db = new ArticleEntities())
        //    {
        //        article = db.Articles.Include("StudentAddress")
        //            .Where(x => x.ArticleID == id)
        //            .Select(x => new ArticleViewModel()
        //            {
        //                ArticleID = x.ArticleID,
        //                Title = x.Title,
        //                ByLine = x.ByLine,
        //                isCarousel = x.isCarousel,
        //                isImportant = x.isImportant,
        //                PageContent = x.PageContent,
        //                Author = x.Author,
        //                PublishStartDate = x.PublishStartDate,
        //                PublishStartTime = x.PublishStartTime,
        //                PublishEndDate = x.PublishEndDate,
        //                PublishEndTime = x.PublishEndTime,
        //                Category = x.Category,
        //                ImageHeader = x.imageHeader,
        //                isDeleted = x.isDeleted
        //            }).FirstOrDefault<ArticleViewModel>();
        //    }

        //    if (article == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(article);
        //}

        [Route("GetArticle/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetArticle(int id)
        {
            IList<ArticleViewModel> articles = null;

            using (var db = new ArticleEntities())
            {
                articles = db.Articles.Select(x => new ArticleViewModel()
                {
                    ArticleID = x.ArticleID,
                    Title = x.Title,
                    ByLine = x.ByLine,
                    isCarousel = x.isCarousel,
                    isImportant = x.isImportant,
                    PageContent = x.PageContent,
                    Author = x.Author,
                    PublishStartDate = x.PublishStartDate,
                    PublishStartTime = x.PublishStartTime,
                    PublishEndDate = x.PublishEndDate,
                    PublishEndTime = x.PublishEndTime,
                    Category = x.Category,
                    ImageHeader = x.imageHeader,
                    isDeleted = x.isDeleted
                }).Where(x => x.ArticleID == id).ToList<ArticleViewModel>();
            }


            //if (articles.Count == 0)
            //{
            //    return NotFound();
            //}

            return Ok(articles);
        }

        public IHttpActionResult AddArticle(ArticleViewModel article)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid article.");

            using (var db = new ArticleEntities())
            {
                db.Articles.Add(new Article()
                {

                ArticleID = article.ArticleID,
                Title = article.Title,
                ByLine = article.ByLine,
                isCarousel = article.isCarousel,
                isImportant = article.isImportant,
                PageContent = article.PageContent,
                Author = article.Author,
                PublishStartDate = article.PublishStartDate,
                PublishStartTime = article.PublishStartTime,
                PublishEndDate = article.PublishEndDate,
                PublishEndTime = article.PublishEndTime,
                Category = article.Category,
                imageHeader = article.ImageHeader,
                isDeleted = article.isDeleted
            });

                db.SaveChanges();
            }

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult EditArticle(ArticleViewModel article)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid article");

            using (var db = new ArticleEntities())
            {
                var existingArticle = db.Articles.Where(x => x.ArticleID == article.ArticleID)
                                                        .FirstOrDefault<Article>();

                if (existingArticle != null)
                {
                    existingArticle.Title = article.Title;
                    existingArticle.ByLine = article.ByLine;
                    existingArticle.isCarousel = article.isCarousel;
                    existingArticle.isImportant = article.isImportant;
                    existingArticle.PageContent = article.PageContent;
                    existingArticle.Author = article.Author;
                    existingArticle.PublishStartDate = article.PublishStartDate;
                    existingArticle.PublishStartTime = article.PublishStartTime;
                    existingArticle.PublishEndDate = article.PublishEndDate;
                    existingArticle.PublishEndTime = article.PublishEndTime;
                    existingArticle.Category = article.Category;
                    existingArticle.imageHeader = article.ImageHeader;
                    existingArticle.isDeleted = article.isDeleted;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        public IHttpActionResult DeleteArticle(int articleID)
        {
            if (articleID <= 0)
                return BadRequest("Not a valid article id");

            using (var db = new ArticleEntities())
            {
                var article = db.Articles
                    .Where(x => x.ArticleID == articleID)
                    .FirstOrDefault();

                db.Entry(article).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }

            return Ok();
        }
    }
}
