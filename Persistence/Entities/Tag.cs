using System.Collections.Generic;

namespace EasyBlog.Persistence.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();

    }
}
