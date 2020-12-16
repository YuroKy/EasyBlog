using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EasyBlog.Models;
using EasyBlog.Persistence;
using EasyBlog.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyBlog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BlogContext _context;

        public UsersController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Users
                .Select(u => new UserDto
                {
                    Username = u.Username,
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    RegistrationTime = u.RegistrationTime,
                    Email = u.Email,
                    Status = u.Status,
                    Avatar = u.Avatar,
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _context.Users
                .Select(u => new UserDto
                {
                    Username = u.Username,
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    RegistrationTime = u.RegistrationTime,
                    Email = u.Email,
                    Status = u.Status,
                    Avatar = u.Avatar,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UserCreateUpdateDto user)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(t => t.Id == id);
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.Email = user.Email;
            userEntity.Username = user.Username;

            if (user.Avatar != null)
            {
                using (var ms = new MemoryStream())
                {
                    await user.Avatar.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    userEntity.Avatar = Convert.ToBase64String(fileBytes);
                }
            }

            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UserCreateUpdateDto user)
        {
            var (passwordHash, passwordSalt) = GetHashedPassword(user.Password);
            string avatar = null;

            if (user.Avatar != null)
            {
                using (var ms = new MemoryStream())
                {
                    await user.Avatar.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    avatar = Convert.ToBase64String(fileBytes);
                }
            }

            await _context.Users.AddAsync(new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RegistrationTime = DateTime.Now,
                Password = passwordHash,
                Salt = passwordSalt,
                Email = user.Email,
                Status = UserStatus.Active,
                Avatar = avatar,
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("change-password/{id}")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] UserChangePasswordDto user)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(t => t.Id == id);
            var (passwordHash, passwordSalt) = GetHashedPassword(user.Password);

            userEntity.Password = passwordHash;
            userEntity.Salt = passwordSalt;

            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("block/{id}")]
        public async Task<IActionResult> BlockUser(Guid id)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(t => t.Id == id);
            userEntity.Status = UserStatus.Blocked;

            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("unblock/{id}")]
        public async Task<IActionResult> UnblockUser(Guid id)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(t => t.Id == id);
            userEntity.Status = UserStatus.Active;

            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private (byte[], byte[]) GetHashedPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var (passwordHash, passwordSalt) = (hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);
                return (passwordHash, passwordSalt);
            }
        }
    }
}