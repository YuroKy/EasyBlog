using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyBlog.Models
{
    public class PostCreateEditDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Guid> TagIds { get; set; }
    }
}