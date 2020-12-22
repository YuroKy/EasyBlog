using System;
using System.Collections.Generic;

namespace EasyBlog.Persistence.Entities
{
    public class Post: BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();


        public Guid? SourceId { get; set; }
        public Source Source { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }
    }
}