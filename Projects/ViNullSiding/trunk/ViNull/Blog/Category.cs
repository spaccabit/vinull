using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViNull.Blog {
    public class CategoryCollection : List<Category> { }

    public class Category {
        public Guid CategoryID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Blog Blog { get; set; }
    }
}
