using Microsoft.AspNetCore.Http;

namespace EasyBlog.Models
{
    public class UserCreateUpdateDto
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
