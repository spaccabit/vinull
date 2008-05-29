using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViNull.Blog {
    public class TagCollection : List<Tag> { }

    public class Tag {
        public Guid TagID { get; set; }
        public String TagName { get; set; }
    }
}
