//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Article
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string ByLine { get; set; }
        public Nullable<bool> isCarousel { get; set; }
        public Nullable<bool> isImportant { get; set; }
        public string PageContent { get; set; }
        public string Author { get; set; }
        public Nullable<System.DateTime> PublishStartDate { get; set; }
        public string PublishStartTime { get; set; }
        public Nullable<System.DateTime> PublishEndDate { get; set; }
        public string PublishEndTime { get; set; }
        public string Category { get; set; }
        public string imageHeader { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    }
}
