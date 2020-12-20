using System.Collections.Generic;

namespace EasyBlog.Persistence.Entities
{
    public class Source : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}