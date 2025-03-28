//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TechBlogWebsite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int CommentID { get; set; }
        public Nullable<int> PostID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Content { get; set; }
        public System.DateTime PublishedDate { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string Link { get; set; }
        public string Meta { get; set; }
        public Nullable<bool> Hide { get; set; }
        public Nullable<int> Order { get; set; }
        public Nullable<System.DateTime> DateBegin { get; set; }
    
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
