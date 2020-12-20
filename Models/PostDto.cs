using System;
using System.Collections.Generic;
using EasyBlog.Persistence.Entities;

namespace EasyBlog.Models
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAvatar { get; set; }
        public SourceDto Source { get; set; }

        public DateTime CreatedTime { get; set; }

        public List<TagDto> Tags { get; set; }
    }
}