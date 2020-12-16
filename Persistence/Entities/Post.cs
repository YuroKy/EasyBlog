using System;
using System.Collections.Generic;

namespace EasyBlog.Persistence.Entities
{
    public class Post: BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();


        public Guid AuthorId { get; set; }
        public User Author { get; set; }
    }
}