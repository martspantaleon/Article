using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Article.Models
{
    public class ArticleViewModel
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string ByLine { get; set; }
        public Nullable<bool> isCarousel { get; set; }
        public Nullable<bool> isImportant { get; set; }
        public string PageContent { get; set; }
        public string Author { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PublishStartDate { get; set; }
        public string PublishStartTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PublishEndDate { get; set; }
        public string PublishEndTime { get; set; }
        public string Category { get; set; }
        public string ImageHeader { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    }
}