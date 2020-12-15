using System;
using EasyBlog.Persistence.Entities;

namespace EasyBlog.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationTime { get; set; }
        public UserStatus Status { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}