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
    
    public partial class Reply
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reply()
        {
            this.Likes = new HashSet<Like>();
            this.Likes1 = new HashSet<Like>();
            this.Likes2 = new HashSet<Like>();
        }
    
        public int ReplyID { get; set; }
        public Nullable<int> TopicID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Content { get; set; }
        public System.DateTime PublishedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int IsLiked { get; set; }
        public string Link { get; set; }
        public string Meta { get; set; }
        public Nullable<bool> Hide { get; set; }
        public Nullable<int> Order { get; set; }
        public Nullable<System.DateTime> DateBegin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Like> Likes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Like> Likes1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Like> Likes2 { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual Topic Topic1 { get; set; }
        public virtual Topic Topic2 { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
    }
}
