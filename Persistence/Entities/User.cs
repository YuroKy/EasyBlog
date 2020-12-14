using System.ComponentModel.DataAnnotations.Schema;

namespace EasyBlog.Persistence.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RegistrationTime { get; set; }
        public string Password { get; set; }

        [NotMapped] public string FullName => $"{FirstName} {LastName}";
    }
}
