using System;

namespace EasyBlog.Persistence.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}