using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyBlog.Persistence.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationTime { get; set; }
        public string Avatar { get; set; }

        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }

        public UserStatus Status { get; set; }

        [NotMapped] public string FullName => $"{FirstName} {LastName}";
    }

    public enum UserStatus
    {
        Active,
        Blocked,
    }
}
