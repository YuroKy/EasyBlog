using System;

namespace EasyBlog.Persistence.Entities
{
    public abstract class BaseEntity: IEntity
    {
        public Guid Id { get; set; }
    }
}