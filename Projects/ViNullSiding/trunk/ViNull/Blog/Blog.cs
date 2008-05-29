using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViNull.Blog {
    public class BlogCollection : List<Blog> { }
    
    public class Blog {
        public Guid BlogID { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
    }
}
