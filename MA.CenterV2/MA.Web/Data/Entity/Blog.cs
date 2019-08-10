using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MA.Web.Data.Entity
{
    public partial class Blog
    {
        public Blog()
        {
            Post = new HashSet<Post>();
        }

        public int BlogId { get; set; }
        public string Url { get; set; }

        public ICollection<Post> Post { get; set; }
    }
}
