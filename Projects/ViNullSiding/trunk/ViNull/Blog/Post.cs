using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViNull.Blog {
    public class PostCollection : List<Post> { }

    public class Post {
        public Guid PostID { get; set; }
        public String Title { get; set; }
        public String Preview { get; set; }
        public String Body { get; set; }
        public String Author { get; set; }
        public String AuthorUsername { get; set; }
        public String AuthotEmail { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime LastModified { get; set; }
        public Boolean Draft { get; set; }
        public String Link { get; set; }

        public CategoryCollection Categories { get; set; }
        public CommentCollection Comments { get; set; }
        public TagCollection Tags { get; set; }

        public Post() {
            Categories = new CategoryCollection();
            Comments = new CommentCollection();
            Tags = new TagCollection();
        }
    }
}
