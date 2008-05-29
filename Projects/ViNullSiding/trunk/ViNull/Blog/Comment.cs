using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViNull.Blog {
    public class CommentCollection : List<Comment> { }

    public class Comment {
        public Guid CommentID { get; set; }
        public String CommentBody { get; set; }
        public String Author { get; set; }
        public String AuthorEmail { get; set; }
        public String AuthorLink { get; set; }
        public DateTime PostedOn { get; set; }
        public Boolean Approved { get; set; }
    }
}
